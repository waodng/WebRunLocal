using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebRunLocal.Utils;

namespace WebRunLocal.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        public bool IsCheck { get; set; }

        /// <summary>
        /// 执行请求方法体之前的事件
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var actionFilters = actionContext.ActionDescriptor.GetCustomAttributes<NoActionFilterAttribute>();
            var controllerFilters = actionContext.ActionDescriptor.GetCustomAttributes<NoActionFilterAttribute>();
            if (actionFilters.Count > 0 | controllerFilters.Count > 0)
            {
                return;
            }

            bool pramaterLoggerPrint = bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]);

            if (pramaterLoggerPrint)
            {
                Stopwatch stopWatch = new Stopwatch();
                actionContext.Request.Properties["action"] = stopWatch;
                stopWatch.Start();

                string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = actionContext.ActionDescriptor.ActionName;
                string requestParameter = JsonConvert.SerializeObject(actionContext.ActionArguments);
                LoggerHelper.WriteLog(string.Format("{0}.{1}{2}入参:{3}", controllerName, actionName, Environment.NewLine, requestParameter));
            }
        }


        /// <summary>
        /// 执行请求方法体之后的事件
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var actionFilters = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<NoActionFilterAttribute>();
            var controllerFilters = actionExecutedContext.ActionContext.ActionDescriptor.GetCustomAttributes<NoActionFilterAttribute>();
            if (actionFilters.Count > 0 | controllerFilters.Count > 0)
            {
                return;
            }

            bool pramaterLoggerPrint = bool.Parse(ConfigurationManager.AppSettings["PramaterLoggerPrint"]);
            if (pramaterLoggerPrint)
            {
                Stopwatch stopWatch = actionExecutedContext.Request.Properties["action"] as Stopwatch;
                stopWatch.Stop();

                string controllerName = actionExecutedContext.ActionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;
                string responseResult = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;

                LoggerHelper.WriteLog(string.Format("{0}.{1}{2}出参:{3}", controllerName, actionName, Environment.NewLine, responseResult));
            }
        }
    }
}
