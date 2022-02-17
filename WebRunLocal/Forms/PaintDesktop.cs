using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace PrintCreator
{
    /// <summary>
    /// 标尺
    /// </summary>
    public partial class PaintDesktop : Panel
    {
        public PaintDesktop()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            base.UpdateStyles();

            //VerticalScroll.SmallChange = 1;
            //VerticalScroll.LargeChange = 1;
            //HorizontalScroll.SmallChange = 1;
            //HorizontalScroll.LargeChange = 1;

            rfb = new SolidBrush(rfcolor);
            f = new Font("宋体", 7);
            p = new Pen(new SolidBrush(rccolor), 1);
            psw1 = new Pen(new SolidBrush(Color.White), 1);
        }

        private readonly SolidBrush rfb = null;
        private Font f = null;
        private readonly Pen p = null;
        private readonly Pen psw1 = null;

        private Color rccolor = Color.Black;

        [Browsable(true), Category("A-宏软"), Description("标尺刻度颜色")]
        public Color RuleCColor
        {
            get
            {
                return rccolor;
            }
            set
            {
                rccolor = value;
                Invalidate();
            }
        }

        private Color rfcolor = Color.Black;

        [Browsable(true), Category("A-宏软"), Description("标尺文字颜色")]
        public Color RuleFontColor
        {
            get
            {
                return rfcolor;
            }
            set
            {
                rfcolor = value;
                Invalidate();
            }
        }

        private float dpiscale = 25.4f;

        [Browsable(true), Category("A-宏软"), Description("缩放比例")]
        public float DpiScale
        {
            get
            {
                return dpiscale;
            }
            set
            {
                if (value <= 0)
                {
                    dpiscale = 25.4f;
                }
                else
                {
                    dpiscale = value;
                }
                Invalidate();
            }
        }

        private bool showNum = true;

        [Browsable(true), Category("A-宏软"), Description("是否显示标尺数字")]
        public bool ShowNum
        {
            get
            {
                return showNum;
            }
            set
            {
                showNum = value;
                Invalidate();
            }
        }

        [Browsable(true), Category("A-宏软"), Description("标尺字体")]
        public Font NumFont
        {
            get
            {
                return f;
            }
            set
            {
                f = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw(e);
        }

        private int Calc(int len)
        {
            return Convert.ToInt32((double)(len) * CreateGraphics().DpiX / dpiscale);
        }

        private Graphics g = null;
        private int padding = 35;

        private bool inner = false;

        [Browsable(true), Category("A-宏软"), Description("标尺位置")]
        public bool Inner
        {
            get
            {
                return inner;
            }
            set
            {
                inner = value;
                Invalidate();
            }
        }

        private void Draw(PaintEventArgs e)
        {
            g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            var xstart = padding - scrollX;
            var ystart = padding - scrollY;
            g.DrawLine(p, new Point(xstart, 0 - scrollY), new Point(xstart, Height));//边缘垂直白线
            g.DrawLine(p, new Point(0 - scrollX, ystart), new Point(Width, ystart));//边缘水平白线

            int x = padding - 24;
            var xs = x - scrollX;
            if (showNum)
            {
                var text = "0";

                g.DrawString(text, f, rfb, new PointF(xstart - 1, x - scrollY));

                SizeF sf = g.MeasureString(text, f);
                Matrix matrixold = g.Transform;
                Matrix matrix = g.Transform;

                matrix.RotateAt(-90, new PointF(x - scrollX + sf.Width / 2, ystart - 1 + sf.Height / 2));
                g.Transform = matrix;

                g.DrawString(text, f, rfb, new PointF(x - scrollX, ystart - 1));
                g.Transform = matrixold;
            }

            //X轴
            for (int i = 1; i < Width; i++)
            {
                int cc = Calc(i) + xstart;

                Point pointx;
                if (inner)
                {
                    pointx = new Point(cc, 0 - scrollY);
                }
                else
                {
                    pointx = new Point(cc, padding - 1 - scrollY);
                }

                if (i % 5 != 0 && i % 10 != 0)
                {
                    if (inner)
                    {
                        g.DrawLine(p, pointx, new Point(cc, 4 - scrollY));//短
                    }
                    else
                    {
                        g.DrawLine(p, new Point(cc, ystart - 5), pointx);//短
                    }
                }

                if (i % 5 == 0 && i % 10 != 0)
                {
                    if (inner)
                    {
                        g.DrawLine(p, pointx, new Point(cc, 7 - scrollY));//中
                    }
                    else
                    {
                        g.DrawLine(p, new Point(cc, ystart - 8), pointx);//中
                    }
                    continue;
                }

                if (i % 10 == 0)
                {
                    if (inner)
                    {
                        g.DrawLine(p, pointx, new Point(cc, 10 - scrollY));//长
                    }
                    else
                    {
                        g.DrawLine(p, new Point(cc, ystart - 11), pointx);//中
                    }

                    if (showNum)
                    {
                        g.DrawString(i.ToString(), f, rfb, new PointF(cc - 2, x - scrollY));
                    }
                    continue;
                }
            }

            //Y轴
            for (int i = 1; i < Height; i++)
            {
                int cc = Calc(i) + ystart;

                Point pointx;
                if (inner)
                {
                    pointx = new Point(0 - scrollX, cc);
                }
                else
                {
                    pointx = new Point(xstart - 1, cc);
                }

                if (i % 5 != 0 && i % 10 != 0)
                {
                    if (inner)
                    {
                        g.DrawLine(p, pointx, new Point(4 - scrollX, cc));//短
                    }
                    else
                    {
                        g.DrawLine(p, new Point(xstart - 5, cc), pointx);//短
                    }
                }

                if (i % 5 == 0 && i % 10 != 0)
                {
                    if (inner)
                    {
                        g.DrawLine(p, pointx, new Point(7 - scrollX, cc));//中
                    }
                    else
                    {
                        g.DrawLine(p, new Point(xstart - 8, cc), pointx);//短
                    }
                    continue;
                }

                if (i % 10 == 0)
                {
                    if (inner)
                    {
                        g.DrawLine(p, pointx, new Point(10 - scrollX, cc));//长
                    }
                    else
                    {
                        g.DrawLine(p, new Point(xstart - 11, cc), pointx);//短
                    }

                    if (showNum)
                    {
                        var text = i.ToString();
                        SizeF sf = g.MeasureString(text, f);
                        Matrix matrixold = g.Transform;
                        Matrix matrix = g.Transform;
                        var xe = cc - 2;
                        matrix.RotateAt(-90, new PointF(xs + sf.Width / 2, xe + sf.Height / 2));
                        g.Transform = matrix;
                        g.DrawString(text, f, rfb, new PointF(xs, xe));
                        g.Transform = matrixold;
                    }
                    continue;
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // 重载基类的背景擦除函数，
            base.OnPaintBackground(e);
        }

        private int scrollX = 0;
        private int scrollY = 0;

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e); Invalidate();

            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
            {
                scrollX = e.NewValue;
            }
            else
            {
                scrollY = e.NewValue;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            scrollY = VerticalScroll.Value;
            scrollX = HorizontalScroll.Value;
            Invalidate();
        }
    }
}