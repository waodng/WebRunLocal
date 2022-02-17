using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Printing;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using ZXing;
using ZXing.QrCode;
using Microsoft.Win32;
using System.Reflection;

/* ==============================================================================
 * 创建日期：2022-02-16 18:42:25
 * 创 建 者：wgd
 * 功能描述：Print  
 * ==============================================================================*/
namespace WebRunLocal.Services.LabelPrint
{
    public class PrintService
    {
        string PrintName;// @"Argox OS-2140 PPLB"
        string PrintType;
        string i_runame;
        string i_ksmc;
        string i_mjff;
        string i_rutype;
        string i_mjdate;
        string i_enddate;
        string i_fid;
        string i_bdid;
        string i_sycs;
        string i_type;
        string i_mjpc;
        string i_mjgh;
        string i_dwname; //外来器械单位名称
        string i_ssname;  //手术名称
        string i_brid; //病人ID号
        string i_brname; //病人姓名
        string i_bzl; //备注栏
        string i_zyqxb;
        string i_gsptype;
        string i_xhcs;
        string i_kwm;
        string i_fzbj;//标签复制打印出的做标记


        private List<PrintModel> PrintInfo = new List<PrintModel>();
        private List<bnqxInfo> i_bnqx = new List<bnqxInfo>();

        #region 条码绘制
    
        //指定打印机
        public void setPrint(string printname)
        {
            PrintName = printname;
        }

        /// <summary>
        /// 指定打印机名和类型以及参数
        /// </summary>
        /// <param name="printName"打印机名称</param>
        /// <param name="printType">打印类型</param>
        /// <param name="paraJson">打印json参数</param>
        public void PrintStart(string printName, string printType, string paraJson)
        {
            PrintInfo = PrintModel.JsonPaser(paraJson);

            if (printName.Length > 0 || printType.Length > 0)
            {
                PrintInfo.ForEach(row =>
                {
                    if (printName.Length > 0)
                    {
                        row.PrintName = printName;
                    }
                    if (printType.Length > 0)
                    {
                        row.PrintType = printType;
                    }
                });
            }
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = PrintName;
            pd.PrintPage += new PrintPageEventHandler(pd_PrintRender);
            pd.Print();
        }

        /// <summary>
        /// 打印页面样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pd_PrintRender(object sender, PrintPageEventArgs e)
        {
            foreach (PrintModel item in PrintInfo)
            {
                switch (item.PrintType)
                {
                    case "器械打包":
                        printQXDB(item, e);
                        break;
                    case "标签复制":
                        break;
                    default:
                        break;
                }
            }
        }

        #region 打印样式合集

        /// <summary>
        /// 器械打包
        /// </summary>
        private void printQXDB(PrintModel printInfo, PrintPageEventArgs e)
        {
            #region 参数设置

            i_runame = printInfo.i_runame;
            i_ksmc = printInfo.i_ksmc;
            i_mjff = printInfo.i_mjff;
            i_rutype = printInfo.i_rutype;
            i_mjdate = "灭菌日期:" + printInfo.i_mjdate.Replace("-", "");
            i_enddate = "失效日期:" + printInfo.i_enddate.Replace("-", "");
            i_fid = "配包人:" + printInfo.i_fid + "  审核人:" + printInfo.i_check;
            i_bdid = "P-" + printInfo.i_bdid;
            if (printInfo.i_bnqx != null)
                i_bnqx = printInfo.i_bnqx;

            #endregion

            //第一行
            e.Graphics.DrawString(i_runame, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 65, 10);
            e.Graphics.DrawString(i_ksmc, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 140, 10);
            e.Graphics.DrawString(i_rutype, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 220, 10);
            e.Graphics.DrawString(i_mjff, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 260, 10);

            //第二行
            e.Graphics.DrawString(i_mjdate, new Font("标楷体", 10, FontStyle.Bold), Brushes.Black, 65, 40);
            e.Graphics.FillRectangle(Brushes.Black, 64, 60, 138, 23);
            e.Graphics.DrawString(i_enddate, new Font("方正姚体", 11, FontStyle.Bold), Brushes.White, 65, 60); //失效日期
            e.Graphics.DrawString(i_fid, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 65, 83);

            //包内器械
            int y = 0;
            int bar = 0;
            int cntBar = 1;
            for (int j = 0; j < i_bnqx.Count; j++, y++)
            {
                if (j > 1 && j % 6 == 0)
                {
                    bar++;
                    cntBar++;
                    y = 0;
                }
                int x = 60 + 82 * bar;

                e.Graphics.DrawString(i_bnqx[j].qxName, new Font("标楷体", 6, FontStyle.Bold), Brushes.Black, x, 105 + 10 * y);
                e.Graphics.DrawString(i_bnqx[j].qxCount, new Font("标楷体", 6, FontStyle.Bold), Brushes.Black, (56 - i_bnqx[j].qxCount.Length * 5) + 82 * cntBar, 105 + 10 * y);
            }

            ////主二维码和编号
            e.Graphics.DrawString(i_bdid, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 233, 83);
            if (i_zyqxb != "" && i_zyqxb != null)
            {
                e.Graphics.DrawString(i_zyqxb, new Font("标楷体", 8), Brushes.Black, 233, 103);//重要器械包 
            }
            e.Graphics.DrawImage(CreateQRZxing(i_bdid, 80, 80), 250, 35, 45, 45);

            //小条码和编号
            e.Graphics.DrawImage(CreateQRZxing(i_bdid, 80, 80), 150, 180, 45, 45);
            e.Graphics.DrawString(i_bdid, new Font("标楷体", 8), Brushes.Black, 65, 200);//小条码治疗编号
            e.Graphics.DrawString(i_runame, new Font("标楷体", 8), Brushes.Black, 65, 180);//小条码标题    

            if (i_zyqxb != "" && i_zyqxb != null)
            {
                e.Graphics.DrawString(i_zyqxb, new Font("标楷体", 8), Brushes.Black, 65, 210);//重要器械包 
            }

            e.Graphics.DrawString(i_kwm, new Font("标楷体", 8), Brushes.Black, 20, 5);//小条码标题    
            e.Graphics.DrawString(i_fzbj, new Font("标楷体", 8), Brushes.Black, 290, 4);//小条码标题 

        }

        #endregion


        /// <summary>
        /// 生成二维码图片
        /// </summary>
        /// <param name="text"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Bitmap CreateQRZxing(string text, int width, int height)
        {
            BarcodeWriter writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            QrCodeEncodingOptions options = new QrCodeEncodingOptions()
            {
                DisableECI = true,//设置内容编码
                CharacterSet = "UTF-8",  //设置二维码的宽度和高度
                Width = width,
                Height = height,
                Margin = 1//设置二维码的边距,单位不是固定像素
            };

            writer.Options = options;
            return writer.Write(text);
        }

        public void printSS_GSP(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx, string gsp)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');
            string[] listGSP = gsp.Split('※');

            for (int i = 0; i < listbdid.Length; i++)
            {
                for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                {
                    if (listbdid[i].Length > 0)
                    {
                        i_type = "ss";
                        i_runame = listruname[i];
                        i_ksmc = "科室名称:" + listksmc[i];
                        //i_mjff = "灭菌类型:" + listmjff[i];
                        if (listGSP[i] == "1")
                        {
                            i_mjff = "消毒类型:高水平消毒";
                        }
                        else
                        {
                            i_mjff = "灭菌类型:" + listmjff[i];
                        }

                        if (listGSP[i] == "1")
                        {
                            i_mjdate = "消毒日期:" + listmjdate[i];
                        }
                        else
                        {
                            i_mjdate = "灭菌日期:" + listmjdate[i];
                        }
                        i_rutype = "规格:" + listrutype[i];
                        //i_mjdate = "灭菌日期:" + listmjdate[i];
                        i_enddate = "失效日期:" + listenddate[i];
                        i_fid = "打包人员:" + listfid[i] + "  " + listfid2[i];
                        i_bdid = "P-" + listbdid[i];

                        PrintDocument pd = new PrintDocument();
                        pd.PrinterSettings.PrinterName = PrintName;
                        pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                        //if (i > 0 || j > 0)
                        //{
                        //    System.Threading.Thread.Sleep(3000);
                        //}
                        pd.Print();
                    }
                }
            }
        }

        //器械包打印
        public void printSSDBNew(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx, string zyqxbh, string mjry, string gsp, string xhcs, string xhwp, string BnInfo, string brId, string brName, string ssName, string dwName, string gh, string gc, string arr)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');
            string[] listgsp = gsp.Split('※');
            string[] listmjgh = gh.Split('※');
            string[] listmjgc = gc.Split('※');
            string[] liszyqx = zyqxbh.Split('※');
            string[] array = arr.Split(',');
            string[] listxhcs = xhcs.Split('※');
            string[] listxhwp = xhwp.Split('※');
            string[] listbrid = brId.Split('※');
            string[] listdwname = dwName.Split('※');
            string[] listssname = ssName.Split('※');
            string[] listbrname = brName.Split('※');
            string[] listkwm = new string[] { };

            if (array.Length > 0)
            {
                listkwm = array[3].ToString().Split('※');
                PrintInfo = PrintModel.JsonPaser(array[9].ToString().Replace('↓', ','));
            }

            for (int i = 0; i < listbdid.Length; i++)
            {
                for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                {
                    if (listbdid[i].Length > 0)
                    {
                        if (!string.IsNullOrEmpty(listbrid[i]) && listbrid[i] != "0")
                        {
                            i_type = "wlqx";
                            i_runame = listruname[i];
                            i_dwname = listdwname[i];
                            i_mjdate = "消毒日期:" + listmjdate[i].Replace("-", "");
                            i_enddate = "失效日期:" + listenddate[i].Replace("-", "");
                            i_fid = "配包人:" + listfid[i] + "  审核人:" + listfid2[i];

                            i_ssname = listssname[i];

                            if (!string.IsNullOrEmpty(listbrid[i]) && listbrid[i] != "0")
                            {
                                i_brid = listbrid[i];
                            }
                            else
                            {
                                i_brid = "";
                            }
                            if (!string.IsNullOrEmpty(listbrname[i]) && listbrname[i] != "0")
                            {
                                i_brname = listbrname[i];
                            }
                            else
                            {
                                i_brname = "";

                            }
                            i_kwm = listkwm[i];

                            i_bdid = "P-" + listbdid[i];
                            if (PrintInfo[i].i_bnqx != null)
                                i_bnqx = PrintInfo[i].i_bnqx;

                            PrintDocument pd = new PrintDocument();
                            pd.PrinterSettings.PrinterName = PrintName;
                            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                            pd.Print();
                        }
                        else
                        {

                            if (!string.IsNullOrEmpty(listmjgh[i]) && listmjgh[i] != "")
                            {
                                i_type = "ss2";
                                i_mjpc = "灭菌批次:" + listmjgc[i];
                                i_mjgh = "灭菌锅号:" + listmjgh[i];
                            }
                            else
                            {
                                i_type = "ss";
                            }
                            if (listxhwp[i] == "1")
                            {
                                // i_runame = listruname[i] + "(剩余" + listxhcs[i] + "次)";
                                int Cs = (int.Parse(listxhcs[i])) + 2;
                                i_runame = listruname[i] + "(第" + Cs + "次)";
                            }
                            else
                            {
                                i_runame = listruname[i];
                            }
                            i_ksmc = listksmc[i];
                            if (listgsp[i] == "1")
                            {
                                i_mjff = "高水平消毒";
                            }
                            else
                            {
                                i_mjff = listmjff[i];
                            }

                            i_mjdate = "消毒日期:" + listmjdate[i].Replace("-", "");
                            i_kwm = listkwm[i];

                            i_rutype = listrutype[i];
                            i_enddate = "失效日期:" + listenddate[i].Replace("-", "");
                            i_fid = "配包人:" + listfid[i] + "  审核人:" + listfid2[i];
                            i_bdid = "P-" + listbdid[i];
                            if (!string.IsNullOrEmpty(liszyqx[i]) && liszyqx[i] != "")
                            {
                                i_zyqxb = "Q-" + liszyqx[i];
                            }
                            else
                            {
                                i_zyqxb = "";
                            }

                            if (PrintInfo[i].i_bnqx != null)
                                i_bnqx = PrintInfo[i].i_bnqx;


                            PrintDocument pd = new PrintDocument();
                            pd.PrinterSettings.PrinterName = PrintName;
                            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                            //if (i > 0 || j > 0)
                            //{
                            //    System.Threading.Thread.Sleep(3000);
                            //}
                            pd.Print();
                        }
                    }
                }
            }
        }


        //器械包打印
        public void printSSBQFZ(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx, string zyqxbh, string mjry, string gsp, string xhcs, string xhwp, string BnInfo, string brId, string brName, string ssName, string dwName, string gh, string gc, string arr)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');
            string[] listgsp = gsp.Split('※');
            string[] listmjgh = gh.Split('※');
            string[] listmjgc = gc.Split('※');
            string[] liszyqx = zyqxbh.Split('※');
            string[] array = arr.Split(',');
            string[] listxhcs = xhcs.Split('※');
            string[] listxhwp = xhwp.Split('※');
            string[] listbrid = brId.Split('※');
            string[] listdwname = dwName.Split('※');
            string[] listssname = ssName.Split('※');
            string[] listbrname = brName.Split('※');
            string[] listkwm = new string[] { };

            if (array.Length > 0)
            {
                listkwm = array[3].ToString().Split('※');
                PrintInfo = PrintModel.JsonPaser(array[9].ToString().Replace('↓', ','));
            }

            for (int i = 0; i < listbdid.Length; i++)
            {
                for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                {
                    if (listbdid[i].Length > 0)
                    {
                        if (!string.IsNullOrEmpty(listbrid[i]) && listbrid[i] != "0")
                        {
                            i_type = "wlqx";
                            i_runame = listruname[i];
                            i_dwname = listdwname[i];
                            i_mjdate = "灭菌日期:" + listmjdate[i].Replace("-", "");
                            i_enddate = "失效日期:" + listenddate[i].Replace("-", "");
                            i_fid = "配包人:" + listfid[i] + "  审核人:" + listfid2[i];

                            i_ssname = listssname[i];

                            if (!string.IsNullOrEmpty(listbrid[i]) && listbrid[i] != "0")
                            {
                                i_brid = listbrid[i];
                            }
                            else
                            {
                                i_brid = "";
                            }
                            if (!string.IsNullOrEmpty(listbrname[i]) && listbrname[i] != "0")
                            {
                                i_brname = listbrname[i];
                            }
                            else
                            {
                                i_brname = "";

                            }
                            i_kwm = listkwm[i];
                            i_fzbj = "★";

                            i_bdid = "P-" + listbdid[i];

                            PrintDocument pd = new PrintDocument();
                            pd.PrinterSettings.PrinterName = PrintName;
                            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                            pd.Print();
                        }
                        else
                        {

                            if (!string.IsNullOrEmpty(listmjgh[i]) && listmjgh[i] != "")
                            {
                                i_type = "ss2";
                                i_mjpc = "灭菌批次:" + listmjgc[i];
                                i_mjgh = "灭菌锅号:" + listmjgh[i];
                            }
                            else
                            {
                                i_type = "ss";
                            }
                            if (listxhwp[i] == "1")
                            {
                                // i_runame = listruname[i] + "(剩余" + listxhcs[i] + "次)";
                                int Cs = (int.Parse(listxhcs[i])) + 2;
                                i_runame = listruname[i] + "(第" + Cs + "次)";
                            }
                            else
                            {
                                i_runame = listruname[i];
                            }
                            i_ksmc = listksmc[i];
                            if (listgsp[i] == "1")
                            {
                                i_mjff = "高水平消毒";
                            }
                            else
                            {
                                i_mjff = listmjff[i];
                            }
                            i_mjdate = "消毒日期:" + listmjdate[i].Replace("-", "");
                            i_kwm = listkwm[i];

                            i_rutype = listrutype[i];
                            i_enddate = "失效日期:" + listenddate[i].Replace("-", "");
                            i_fid = "配包人:" + listfid[i] + "  审核人:" + listfid2[i];
                            i_bdid = "P-" + listbdid[i];
                            i_fzbj = "★";
                            if (!string.IsNullOrEmpty(liszyqx[i]) && liszyqx[i] != "")
                            {
                                i_zyqxb = "Q-" + liszyqx[i];
                            }
                            else
                            {
                                i_zyqxb = "";
                            }
                            if (PrintInfo[i].i_bnqx != null)
                                i_bnqx = PrintInfo[i].i_bnqx;

                            PrintDocument pd = new PrintDocument();
                            pd.PrinterSettings.PrinterName = PrintName;
                            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                            pd.Print();
                        }
                    }
                }
            }
        }


        public void printSSAndWlqx(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx, string zyqxb, string gspwp, string xhcs, string xhqx, string brId, string brName, string SsName, string dwName)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');
            string[] liszyqx = zyqxb.Split('※');
            string[] listGSP = gspwp.Split('※');
            string[] listxhcs = xhcs.Split('※');
            string[] listxhwp = xhqx.Split('※');
            string[] listbrid = brId.Split('※');
            string[] listdwname = dwName.Split('※');
            string[] listssname = SsName.Split('※');
            string[] listbrname = brName.Split('※');
            for (int i = 0; i < listbdid.Length; i++)
            {
                for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                {
                    if (listbdid[i].Length > 0)
                    {
                        if (!string.IsNullOrEmpty(listbrid[i]) && listbrid[i] != "0")
                        {
                            i_type = "wlqx";
                            i_runame = listruname[i];
                            i_dwname = listdwname[i];
                            i_mjdate = "灭菌日期:" + listmjdate[i];
                            i_enddate = "失效日期:" + listenddate[i];
                            i_fid = "打包人员:" + listfid[i] + "  " + listfid2[i];

                            i_ssname = listssname[i];

                            if (!string.IsNullOrEmpty(listbrid[i]) && listbrid[i] != "0")
                            {
                                i_brid = listbrid[i];
                            }
                            else
                            {
                                i_brid = "";
                            }
                            if (!string.IsNullOrEmpty(listbrname[i]) && listbrname[i] != "0")
                            {
                                i_brname = listbrname[i];
                            }
                            else
                            {
                                i_brname = "";

                            }

                            i_bdid = "P-" + listbdid[i];

                            PrintDocument pd = new PrintDocument();
                            pd.PrinterSettings.PrinterName = PrintName;
                            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                            pd.Print();
                        }
                        else
                        {


                            i_type = "ss3";
                            if (listxhwp[i] == "1")
                            {
                                int Cs = (int.Parse(listxhcs[i])) + 2;
                                i_runame = listruname[i] + "(第" + Cs + "次)";
                            }
                            else
                            {
                                i_runame = listruname[i];
                            }
                            i_ksmc = "科室名称:" + listksmc[i];
                            i_rutype = "物品规格:" + listrutype[i];
                            if (listGSP[i] == "1")
                            {
                                i_mjff = "消毒类型:高水平消毒";
                            }
                            else
                            {
                                i_mjff = "灭菌类型:" + listmjff[i];
                            }

                            if (listGSP[i] == "1")
                            {
                                i_mjdate = "消毒日期:" + listmjdate[i];
                            }
                            else
                            {
                                i_mjdate = "灭菌日期:" + listmjdate[i];
                            }

                            i_enddate = "失效日期:" + listenddate[i];
                            i_fid = "打包/核对:" + listfid[i] + "/" + listfid2[i];
                            i_bdid = "P-" + listbdid[i];
                            if (liszyqx[i] != "" && liszyqx[i] != null)
                            {
                                i_zyqxb = "Q-" + liszyqx[i];
                            }

                            PrintDocument pd = new PrintDocument();
                            pd.PrinterSettings.PrinterName = PrintName;
                            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                            pd.Print();
                        }
                    }
                }
            }
        }


        public void printSSZYQX(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx, string zyqxb, string xhcs, string xhwp, string gh, string gc, string arr)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');
            string[] liszyqx = zyqxb.Split('※');
            string[] listxhcs = xhcs.Split('※');
            string[] listxhwp = xhwp.Split('※');
            string[] listmjgh = gh.Split('※');
            string[] listmjgc = gc.Split('※');

            string[] array = arr.Split(',');
            if (array.Length > 0)
            {

            }

            for (int i = 0; i < listbdid.Length; i++)
            {
                for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                {
                    if (listbdid[i].Length > 0)
                    {

                        if (!string.IsNullOrEmpty(listmjgh[i]) && listmjgh[i] != "")
                        {
                            i_type = "ss2";
                            i_mjpc = "灭菌批次:" + listmjgc[i];
                            i_mjgh = "灭菌锅号:" + listmjgh[i];
                        }
                        else
                        {
                            i_type = "ss";
                        }
                        if (listxhwp[i] == "1")
                        {

                            //  i_runame = listruname[i] + "(剩余" + listxhcs[i] + "次)";
                            int Cs = (int.Parse(listxhcs[i])) + 2;
                            i_runame = listruname[i] + "(第" + Cs + "次)";

                        }
                        else
                        {
                            i_runame = listruname[i];
                        }

                        i_ksmc = "科室名称:" + listksmc[i];
                        i_mjff = "灭菌类型:" + listmjff[i];
                        i_rutype = "规格:" + listrutype[i];
                        i_mjdate = "灭菌日期:" + listmjdate[i];
                        i_enddate = "失效日期:" + listenddate[i];
                        i_fid = "打包人员:" + listfid[i] + "  " + listfid2[i];
                        i_bdid = "P-" + listbdid[i];

                        if (liszyqx[i] != "" && liszyqx[i] != null)
                        {
                            i_zyqxb = "Q-" + liszyqx[i];
                        }

                        PrintDocument pd = new PrintDocument();
                        pd.PrinterSettings.PrinterName = PrintName;
                        pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                        //if (i > 0 || j > 0)
                        //{
                        //    System.Threading.Thread.Sleep(3000);
                        //}
                        pd.Print();
                    }
                }
            }
        }

        //高水平的标签打印
        public void printGSP2(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');

            for (int i = 0; i < listbdid.Length; i++)
            {
                //for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                //{
                if (listbdid[i].Length > 0)
                {
                    i_type = "gsp";
                    i_runame = listruname[i];
                    i_ksmc = "科室名称:" + listksmc[i];
                    i_mjff = "消毒类型:高水平消毒";
                    i_rutype = "物品规格:" + listrutype[i];
                    i_mjdate = "消毒日期:" + listmjdate[i];
                    //i_enddate = "失效日期:" + listenddate[i];
                    i_fid = "打包/核对:" + listfid[i];
                    i_bdid = listbdid[i];

                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = PrintName;
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);

                    pd.Print();
                }
                //   }
            }
        }
        //日常监测包
        public void printRC(string bdid)
        {
            string[] listbdid = bdid.Split('※');

            for (int i = 0; i < listbdid.Length; i++)
            {
                i_type = "rc";
                i_bdid = "R-" + listbdid[i];

                if (listbdid[i].Length > 0)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = PrintName;
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    if (i > 0)
                    {
                        System.Threading.Thread.Sleep(1000);
                    }
                    pd.Print();
                }
            }
        }

        //日常监测包(PCD)
        public void printRCPCD(string bdid, string sycs)
        {
            string[] listbdid = bdid.Split('※');
            string[] listsycs = sycs.Split('※');
            for (int i = 0; i < listbdid.Length; i++)
            {
                i_type = "rcpcd";
                i_bdid = "R-" + listbdid[i];
                i_sycs = "使用次数:" + listsycs[i];
                if (listbdid[i].Length > 0)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = PrintName;
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    if (i > 0)
                    {
                        System.Threading.Thread.Sleep(3000);
                    }
                    pd.Print();
                }
            }
        }
        //BD监测包(PCD)
        public void printBDPCD(string bdid, string sycs)
        {
            string[] listbdid = bdid.Split('※');
            string[] listsycs = sycs.Split('※');
            for (int i = 0; i < listbdid.Length; i++)
            {
                i_type = "bdpcd";
                i_bdid = "B-" + listbdid[i];
                i_sycs = "使用次数:" + listsycs[i];
                if (listbdid[i].Length > 0)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = PrintName;
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    if (i > 0)
                    {
                        System.Threading.Thread.Sleep(3000);
                    }
                    pd.Print();
                }
            }

        }
        //生物监测包
        public void printSWPCD(string bdid)
        {
            string[] listbdid = bdid.Split('※');
            for (int i = 0; i < listbdid.Length; i++)
            {
                i_type = "sw";
                i_bdid = "D-" + listbdid[i];
                if (listbdid[i].Length > 0)
                {
                    PrintDocument pd = new PrintDocument();
                    pd.PrinterSettings.PrinterName = PrintName;
                    pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                    if (i > 0)
                    {
                        System.Threading.Thread.Sleep(3000);
                    }
                    pd.Print();
                }
            }

        }
        //物品标签打印
        public void printWP(string bdid, string name, string dept, string type)
        {
            i_type = "wp";
            string[] runame = name.Split('※');//物品名称
            string[] rutype = type.Split('※');//物品规格
            string[] pid = bdid.Split('※');//编号
            string[] ksmc = dept.Split('※');//科室名称
            for (int i = 0; i < pid.Length; i++)
            {
                i_runame = "物品名称 " + runame[i];
                i_ksmc = "科室名称 " + ksmc[i];
                i_rutype = "物品规格 " + rutype[i];
                i_bdid = "W-" + pid[i];
                PrintDocument pd = new PrintDocument();
                pd.PrinterSettings.PrinterName = PrintName;
                pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                if (i > 0)
                {
                    System.Threading.Thread.Sleep(3000);
                }
                pd.Print();
            }

        }
        //人员标签打印
        public void printRR(string name, string id)
        {
            i_type = "rr";
            i_runame = name;
            i_bdid = id;
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = PrintName;
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.Print();
        }

        public void printSS2(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx, string mjgh, string mjph, string array)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');
            string[] listmjph = mjph.Split('※');
            string[] listmjgh = mjgh.Split('※');
            string[] lstBnwp = array.Split('※');

            for (int i = 0; i < listbdid.Length; i++)
            {
                for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                {
                    if (listbdid[i].Length > 0)
                    {
                        i_type = "ss2";
                        i_runame = listruname[i];
                        i_ksmc = listksmc[i];
                        i_mjff = listmjff[i];
                        i_rutype = listrutype[i];
                        i_mjdate = "灭菌日期:" + listmjdate[i];
                        i_enddate = "失效日期:" + listenddate[i];
                        i_fid = "配包人:" + listfid[i] + "  审核人:" + listfid2[i];
                        i_mjpc = "灭菌批次:" + listmjph[i];
                        i_mjgh = "灭菌锅号:" + listmjgh[i];
                        i_bdid = "P-" + listbdid[i];

                        PrintDocument pd = new PrintDocument();
                        pd.PrinterSettings.PrinterName = PrintName;
                        pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                        //if (i > 0 || j > 0)
                        //{
                        //    System.Threading.Thread.Sleep(3000);
                        //}
                        pd.Print();
                    }
                }
            }
        }

        public void printSS(string runame, string ksmc, string mjff, string rutype, string mjdate, string enddate, string fid, string fid2, string bdid, string wplx, string array)
        {
            string[] listruname = runame.Split('※');
            string[] listksmc = ksmc.Split('※');
            string[] listmjff = mjff.Split('※');
            string[] listrutype = rutype.Split('※');
            string[] listmjdate = mjdate.Split('※');
            string[] listenddate = enddate.Split('※');
            string[] listfid = fid.Split('※');
            string[] listbdid = bdid.Split('※');
            string[] listfid2 = fid2.Split('※');
            string[] listwplx = wplx.Split('※');
            string[] lstBnwp = array.Split('※');

            for (int i = 0; i < listbdid.Length; i++)
            {
                for (int j = 0; j < Convert.ToInt32(listwplx[i]); j++)
                {
                    if (listbdid[i].Length > 0)
                    {
                        i_type = "ss";
                        i_runame = listruname[i];
                        i_ksmc = listksmc[i];
                        i_mjff = listmjff[i];
                        i_rutype = listrutype[i];
                        i_mjdate = "灭菌日期:" + listmjdate[i].Replace("-", "");
                        i_enddate = "失效日期:" + listenddate[i].Replace("-", "");
                        i_fid = "配包人:" + listfid[i] + "  审核人:" + listfid2[i];
                        i_bdid = "P-" + listbdid[i];
                        PrintDocument pd = new PrintDocument();
                        pd.PrinterSettings.PrinterName = PrintName;
                        pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
                        //if (i > 0 || j > 0)
                        //{
                        //    System.Threading.Thread.Sleep(3000);
                        //}
                        pd.Print();
                    }
                }
            }
        }
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            //e.Graphics.Clear(Color.White);
            //e.Graphics.DrawRectangle(new Pen(Brushes.Black), e.PageBounds);

            //治疗包
            if (i_type.Equals("ss"))
            {
                //第一行
                e.Graphics.DrawString(i_runame, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 65, 10);

                e.Graphics.DrawString(i_rutype, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 220, 10);


                //第二行
                e.Graphics.DrawString(i_mjdate, new Font("标楷体", 10, FontStyle.Bold), Brushes.Black, 65, 40);
                e.Graphics.FillRectangle(Brushes.Black, 64, 60, 138, 23);
                e.Graphics.DrawString(i_enddate, new Font("方正姚体", 11, FontStyle.Bold), Brushes.White, 65, 60); //失效日期
                e.Graphics.DrawString(i_fid, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 65, 83);

                //主二维码和编号
                e.Graphics.DrawString(i_bdid, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 233, 83);
                if (i_zyqxb != "" && i_zyqxb != null)
                {
                    e.Graphics.DrawString(i_zyqxb, new Font("标楷体", 8), Brushes.Black, 233, 103);//重要器械包 
                }
                e.Graphics.DrawImage(CreateQRZxing(i_bdid, 80, 80), 233, 30, 55, 55);

                //小条码和编号
                e.Graphics.DrawImage(CreateQRZxing(i_bdid, 80, 80), 150, 100, 55, 55);
                e.Graphics.DrawString(i_bdid, new Font("标楷体", 8), Brushes.Black, 65, 120);//小条码治疗编号
                //e.Graphics.DrawString(i_runame, new Font("标楷体", 8), Brushes.Black, 65, 180);//小条码标题    
                e.Graphics.DrawString(i_ksmc, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 233, 110);
                e.Graphics.DrawString(i_mjff, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 233, 130);

                if (i_zyqxb != "" && i_zyqxb != null)
                {
                    e.Graphics.DrawString(i_zyqxb, new Font("标楷体", 8), Brushes.Black, 65, 110);//重要器械包 
                }

                e.Graphics.DrawString(i_kwm, new Font("标楷体", 8), Brushes.Black, 20, 5);//小条码标题    
                e.Graphics.DrawString(i_fzbj, new Font("标楷体", 8), Brushes.Black, 272, 2);//小条码标题    


            }
            else if (i_type.Equals("ss2"))
            {
                #region 锅号、锅次
                //锅号锅次暂时没有打印
                // e.Graphics.DrawString(i_mjgh + "  " + i_mjpc, new Font("标楷体", 8), Brushes.Black, 65, 57);

                //第一行
                e.Graphics.DrawString(i_runame, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 65, 10);

                e.Graphics.DrawString(i_rutype, new Font("标楷体", 12, FontStyle.Bold), Brushes.Black, 220, 10);


                //第二行
                e.Graphics.DrawString(i_mjdate, new Font("标楷体", 10, FontStyle.Bold), Brushes.Black, 65, 40);
                e.Graphics.FillRectangle(Brushes.Black, 64, 60, 138, 23);
                e.Graphics.DrawString(i_enddate, new Font("方正姚体", 11, FontStyle.Bold), Brushes.White, 65, 60); //失效日期
                e.Graphics.DrawString(i_fid, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 65, 83);

                //包内器械
                int y = 0;
                int bar = 0;
                int cntBar = 1;
                for (int i = 0; i < i_bnqx.Count; i++, y++)
                {
                    if (i > 1 && i % 6 == 0)
                    {
                        bar++;
                        cntBar++;
                        y = 0;
                    }
                    int x = 60 + 82 * bar;

                    e.Graphics.DrawString(i_bnqx[i].qxName, new Font("标楷体", 6, FontStyle.Bold), Brushes.Black, x, 105 + 10 * y);
                    e.Graphics.DrawString(i_bnqx[i].qxCount, new Font("标楷体", 6, FontStyle.Bold), Brushes.Black, (56 - i_bnqx[i].qxCount.Length * 5) + 82 * cntBar, 105 + 10 * y);

                }

                ////主二维码和编号
                e.Graphics.DrawString(i_bdid, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 233, 83);
                if (i_zyqxb != "" && i_zyqxb != null)
                {
                    e.Graphics.DrawString(i_zyqxb, new Font("标楷体", 8), Brushes.Black, 233, 103);//重要器械包 
                }
                e.Graphics.DrawImage(CreateQRZxing(i_bdid, 80, 80), 250, 35, 45, 45);

                //小条码和编号
                e.Graphics.DrawImage(CreateQRZxing(i_bdid, 80, 80), 150, 180, 45, 45);
                e.Graphics.DrawString(i_bdid, new Font("标楷体", 8), Brushes.Black, 65, 200);//小条码治疗编号
                //e.Graphics.DrawString(i_runame, new Font("标楷体", 8), Brushes.Black, 65, 180);//小条码标题   
                e.Graphics.DrawString(i_ksmc, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 233, 180);
                e.Graphics.DrawString(i_mjff, new Font("标楷体", 8, FontStyle.Bold), Brushes.Black, 233, 200);

                if (i_zyqxb != "" && i_zyqxb != null)
                {
                    e.Graphics.DrawString(i_zyqxb, new Font("标楷体", 8), Brushes.Black, 65, 210);//重要器械包 
                }

                e.Graphics.DrawString(i_kwm, new Font("标楷体", 8), Brushes.Black, 20, 5);//小条码标题    
                e.Graphics.DrawString(i_fzbj, new Font("标楷体", 8), Brushes.Black, 275, 2);//小条码标题 

                #endregion
            }
        }
        #endregion
    }
}
