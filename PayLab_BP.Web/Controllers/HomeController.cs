using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace PayLab_BP.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : PayLab_BPControllerBase
    {
        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }
	}
}