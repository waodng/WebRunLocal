using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebRunLocal.Utils;

namespace WebRunLocal.Filters
{
    /// <summary>
    /// 该属性指示该action是否被拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NoActionFilterAttribute : Attribute
    {
        public NoActionFilterAttribute()
        {

        }
    }
}
