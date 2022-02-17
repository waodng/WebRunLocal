using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebRunLocal.Filters;
using WebRunLocal.Services.LabelPrint;

namespace WebRunLocal.Controllers
{
    /// <summary>
    ///  打印控制逻辑
    /// </summary>
    public class PrintController : BaseApiController
    {
        /// <summary>
        /// 器械打包接口
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Qxdb([FromBody]object message)
        {
            PrintService print = new PrintService();
            print.setPrint("gys");
            print.PrintStart("", "器械打包", "[{\"PrintName\":\"//192.168.3.105//gys\",\"PrintType\":\"器械打包\",\"i_runame\":\"护目镜\",\"i_ksmc\":\"供应室\",\"i_mjff\":\"低温灭菌\",\"i_rutype\":\"\",\"i_rukind\":\"11\",\"i_mjdate\":\"2021-04-12\",\"i_enddate\":\"2021-07-11\",\"i_fid\":\"0001\",\"i_check\":\"0001\",\"i_bdid\":\"441257\",\"i_sycs\":\"0\",\"i_mjpc\":\"\",\"i_mjgh\":\"\",\"i_mjry\":\"\",\"i_dwname\":\"\",\"i_ssname\":\"\",\"i_brid\":\"\",\"i_brname\":\"\",\"i_bzl\":\"\",\"i_zyqxb\":\"\",\"i_gsptype\":\"1\",\"i_xhcs\":\" \",\"i_kwm\":null,\"i_fzbj\":null,\"i_count\":\"1\",\"i_bnqx\":[{\"qxName\":\"生物指示剂\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"直肠洞\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"中方巾\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"子宫刮匙\",\"qxCount\":\"3\",\"descript\":null},{\"qxName\":\"中白包布\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"拆线剪\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"中碗\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"自封袋\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"组织剪\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"治疗巾\",\"qxCount\":\"3\",\"descript\":null},{\"qxName\":\"胃洞\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"绿色无纺布\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"弯盘\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"三切口洞\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"手术纱布巾\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"手术纱布巾\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"神经拉钩\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"消耗物品测试\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"眼洞\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"腰穿测压管\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"眼科剪\",\"qxCount\":\"1\",\"descript\":null}]},{\"PrintName\":\"//192.168.3.105//gys\",\"PrintType\":\"器械打包\",\"i_runame\":\"护目镜\",\"i_ksmc\":\"供应室\",\"i_mjff\":\"低温灭菌\",\"i_rutype\":\"\",\"i_rukind\":\"11\",\"i_mjdate\":\"2021-04-12\",\"i_enddate\":\"2021-07-11\",\"i_fid\":\"0001\",\"i_check\":\"0001\",\"i_bdid\":\"441257\",\"i_sycs\":\"0\",\"i_mjpc\":\"\",\"i_mjgh\":\"\",\"i_mjry\":\"\",\"i_dwname\":\"\",\"i_ssname\":\"\",\"i_brid\":\"\",\"i_brname\":\"\",\"i_bzl\":\"\",\"i_zyqxb\":\"\",\"i_gsptype\":\"1\",\"i_xhcs\":\" \",\"i_kwm\":null,\"i_fzbj\":null,\"i_count\":\"1\",\"i_bnqx\":[{\"qxName\":\"生物指示剂\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"直肠洞\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"中方巾\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"子宫刮匙\",\"qxCount\":\"3\",\"descript\":null},{\"qxName\":\"中白包布\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"拆线剪\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"中碗\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"自封袋\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"组织剪\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"治疗巾\",\"qxCount\":\"3\",\"descript\":null},{\"qxName\":\"胃洞\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"绿色无纺布\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"弯盘\",\"qxCount\":\"2\",\"descript\":null},{\"qxName\":\"三切口洞\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"手术纱布巾\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"手术纱布巾\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"神经拉钩\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"消耗物品测试\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"眼洞\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"腰穿测压管\",\"qxCount\":\"1\",\"descript\":null},{\"qxName\":\"眼科剪\",\"qxCount\":\"1\",\"descript\":null}]}]");
          
            HttpResponseMessage resonse = new HttpResponseMessage
            {
                Content = new StringContent("打印成功！", Encoding.UTF8, "application/json")
            };
            return resonse;
        }
    }
}
