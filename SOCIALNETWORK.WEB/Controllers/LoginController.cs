using Newtonsoft.Json;
using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.Login;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SOCIALNETWORK.WEB.Controllers
{
    [RoutePrefix("")]
    public class LoginController : Controller
    {
        private readonly ILoginHandler _loginHandler;

        public LoginController(ILoginHandler loginHandler)
        {
            _loginHandler = loginHandler;
        }

        [Route("")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index","Home");

            return View();
        }

        [Route("registro")]
        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                var result = await _loginHandler.RegisterAsync(model);
                return Json(result);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [Route("ingreso")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                var result = await _loginHandler.LoginAsync(model);
                FormsAuthentication.SetAuthCookie($"{result.Id}|{result.Name} {result.PatSurname}|{result.Email}", false);
                return Json(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}