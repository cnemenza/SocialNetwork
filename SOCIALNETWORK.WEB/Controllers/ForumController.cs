using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.Forum;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SOCIALNETWORK.WEB.Controllers
{
    [Authorize]
    [RoutePrefix("foro")]
    public class ForumController : Controller
    {
        private readonly IForumHandler _forumHandler;

        public ForumController(IForumHandler forumHandler)
        {
            _forumHandler = forumHandler;
        }

        [Route("{eventId}")]
        [HttpGet]
        public async Task<ActionResult> Index(Guid eventId)
        {
            var hasAuthorization = await _forumHandler.HasAuthorization(eventId, ClaimsExtension.GetCurrentUserId());
            if (!hasAuthorization)
                return RedirectToAction("Detail", "Event",new { id=eventId});

            ViewBag.EventId = eventId;
            return View();
        }

        [Route("get-mensajes")]
        [HttpGet]
        public async Task<ActionResult> GetMessages(Guid eventId)
            => Json(await _forumHandler.GetMessages(eventId),JsonRequestBehavior.AllowGet);

        [Route("nuevo-mensaje")]
        [HttpPost]
        public async Task<ActionResult> NewMessage(NewMessageViewModel model)
        {
            model.UserId = ClaimsExtension.GetCurrentUserId();
            await _forumHandler.NewMessage(model);
            return Json(string.Empty);
        }
    }
}