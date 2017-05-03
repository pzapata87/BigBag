using System.Web.Mvc;
using SIGCOMT.Web.Core;

namespace SIGCOMT.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}