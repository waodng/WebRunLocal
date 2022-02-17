using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebRunLocal.Services.LabelPrint
{
    /// <summary>
    /// 打印信息实体
    /// </summary>
    public class PrintModel
    {
        /// <summary>
        /// 打印机名称
        /// </summary>
        public string PrintName { get; set; }
        /// <summary>
        /// 打印类型
        /// </summary>
        public string PrintType { get; set; }
        /// <summary>
        /// 物品名称
        /// </summary>
        public string  i_runame{ get; set; }
        /// <summary>
        /// 科室名称
        /// </summary>
        public string i_ksmc { get; set; }
        /// <summary>
        /// 灭菌类型
        /// </summary>
        public string i_mjff { get; set; }
        /// <summary>
        /// 物品规格
        /// </summary>
        public string i_rutype { get; set; }
        /// <summary>
        /// 物品类型
        /// </summary>
        public string i_rukind { get; set; }
        /// <summary>
        /// 灭菌日期
        /// </summary>
        public string i_mjdate { get; set; }
        /// <summary>
        /// 失效日期
        /// </summary>
        public string i_enddate { get; set; }
        /// <summary>
        /// 打包人信息
        /// </summary>
        public string i_fid { get; set; }
        /// <summary>
        /// 核包人信息
        /// </summary>
        public string i_check { get; set; }
        /// <summary>
        /// 治疗包号信息
        /// </summary>
        public string i_bdid { get; set; }
        /// <summary>
        /// 使用次数
        /// </summary>
        public string i_sycs { get; set; }
        /// <summary>
        /// 灭菌人员
        /// </summary>
        public string i_mjry { get; set; }
        /// <summary>
        /// 锅次
        /// </summary>
        public string i_mjpc { get; set; }
        /// <summary>
        /// 锅号
        /// </summary>
        public string i_mjgh { get; set; }
        /// <summary>
        /// 外来器械单位名称
        /// </summary>
        public string i_dwname { get; set; }

        /// <summary>
        /// 手术名称
        /// </summary>
        public string i_ssname { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string i_brid { get; set; }
        /// <summary>
        /// 病人姓名
        /// </summary>
        public string i_brname { get; set; }
        /// <summary>
        /// 备注栏
        /// </summary>
        public string i_bzl { get; set; }

        /// <summary>
        /// 重要器械包号
        /// </summary>
        public string i_zyqxb { get; set; }
        /// <summary>
        /// 高水平类型
        /// </summary>
        public string i_gsptype { get; set; }
        /// <summary>
        /// 消耗次数
        /// </summary>
        public string i_xhcs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string i_kwm { get; set; }
        /// <summary>
        /// 标签复制打印出的做标记
        /// </summary>
        public string i_fzbj { get; set; }
        /// <summary>
        /// 打印张数
        /// </summary>
        public string i_count { get; set; }
        private List<bnqxInfo> _i_bnqx = new List<bnqxInfo>();
        /// <summary>
        /// 包内器械
        /// </summary>
        public List<bnqxInfo> i_bnqx
        {
            get
            {
                return _i_bnqx;
            }
            set
            {
                _i_bnqx = value;
            }
        } 

        /// <summary>
        /// 解析返回的数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<PrintModel> JsonPaser(string json)
        {
            return JsonConvert.DeserializeObject<List<PrintModel>>(json);
        }
    }

    public class bnqxInfo
    {
        /// <summary>
        /// 器械名称
        /// </summary>
        public string qxName { get; set; }
        /// <summary>
        /// 器械数量
        /// </summary>
        public string qxCount { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string descript { get; set; }
    }
}
