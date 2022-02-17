using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using WebRunLocal.Filters;

/* ==============================================================================
 * 创建日期：2022-02-16 11:53:35
 * 创 建 者：wgd
 * 功能描述：BaseApiController  
 * ==============================================================================*/
namespace WebRunLocal.Controllers
{
    [ActionFilter(IsCheck = true)]
    public class BaseApiController : ApiController
    {

    }
}
