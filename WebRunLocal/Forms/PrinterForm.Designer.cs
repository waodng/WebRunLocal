namespace WebRunLocal.Forms
{
    partial class PrinterForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.paintDesktop1 = new PrintCreator.PaintDesktop();
            this.label1 = new System.Windows.Forms.Label();
            this.paintDesktop1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // paintDesktop1
            // 
            this.paintDesktop1.Controls.Add(this.label1);
            this.paintDesktop1.DpiScale = 25.4F;
            this.paintDesktop1.Inner = false;
            this.paintDesktop1.Location = new System.Drawing.Point(12, 52);
            this.paintDesktop1.Name = "paintDesktop1";
            this.paintDesktop1.NumFont = new System.Drawing.Font("宋体", 7F);
            this.paintDesktop1.RuleCColor = System.Drawing.Color.Black;
            this.paintDesktop1.RuleFontColor = System.Drawing.Color.Black;
            this.paintDesktop1.ShowNum = true;
            this.paintDesktop1.Size = new System.Drawing.Size(734, 411);
            this.paintDesktop1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "hello world";
            // 
            // PrinterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 475);
            this.Controls.Add(this.paintDesktop1);
            this.Name = "PrinterForm";
            this.Text = "打印设计";
            this.Load += new System.EventHandler(this.PrinterForm_Load);
            this.paintDesktop1.ResumeLayout(false);
            this.paintDesktop1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private PrintCreator.PaintDesktop paintDesktop1;
        private System.Windows.Forms.Label label1;
    }
}