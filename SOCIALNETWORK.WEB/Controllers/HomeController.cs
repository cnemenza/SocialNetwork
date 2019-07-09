using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SOCIALNETWORK.WEB.Controllers
{
    [Authorize]
    [HandleError]
    [RoutePrefix("")]
    public class HomeController : Controller
    {
        [Route("inicio")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("cerrar-sesion")]
        [HttpGet]
        public ActionResult SignOut()
        {
            if(User.Identity.IsAuthenticated)
                FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Login");
        }
    }
}