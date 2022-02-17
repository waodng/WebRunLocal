using System.Net.Http;
using System.Text;
using System.Web.Http;
using WebRunLocal.Filters;

namespace WebRunLocal.Controllers
{
    /// <summary>
    ///  Hello controller.
    /// </summary>
    [RoutePrefix("api/hello")]
    public class HelloController : BaseApiController
    {
        [Route("getecho")]
        [HttpGet]
        [NoActionFilter]//外界依然可以访问，Action过滤器不执行
        public IHttpActionResult GetEcho(string name)
        {
            return Json(new { Name = name, Message = string.Format("Hello, {0}", name) });
        }

        [HttpGet]
        [NonAction]//外界无法访问，但内部可调用
        public IHttpActionResult Helloworld()                               
        {
            return Json(new { Name = "测试", Message = "Hello, world！" });
        }

        [HttpPost]
        public HttpResponseMessage SendMessage([FromBody]object message)
        {
            HttpResponseMessage resonse = new HttpResponseMessage
            {
                Content = new StringContent(message.ToString(), Encoding.UTF8, "application/json")
            };
            return resonse;
        }
    }
}
