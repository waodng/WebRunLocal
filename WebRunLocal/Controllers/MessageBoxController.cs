using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebRunLocal.Utils;

/* ==============================================================================
 * 创建日期：2022-02-16 15:57:27
 * 创 建 者：wgd
 * 功能描述：MessageBoxController  
 * ==============================================================================*/
namespace WebRunLocal.Controllers
{
    public class MessageBoxController :BaseApiController
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int MessageBox(int hWnd, String text, String caption, uint type);


        /// <summary>
        /// 浏览器端调用messagebox接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Show()
        {
            //byte[] startParamsBuf = Encoding.Default.GetBytes("12345678");
            //IntPtr startParamsPtr = Marshal.AllocHGlobal(startParamsBuf.Length);
            //Marshal.Copy(startParamsBuf, 0, startParamsPtr, startParamsBuf.Length);

            string result;
            try
            {
                MessageBox(0, "Hello Win32 API", "水之真谛", 4);

                result = "调用成功";
            }
            catch (Exception e)
            {
                LoggerHelper.WriteLog("调用上海医保动态库异常", e);

                JObject jObject = JObject.FromObject(new
                {
                    xxfhm = "500",
                    fhxx = "调用医保动态库失败,原因:" + e.Message
                });

                result = jObject.ToString();
            }
            finally
            {
                
            }

            HttpResponseMessage resonse = new HttpResponseMessage
            {
                Content = new StringContent(result, Encoding.UTF8, "application/json")
            };
            return resonse;
        }
    }
}
