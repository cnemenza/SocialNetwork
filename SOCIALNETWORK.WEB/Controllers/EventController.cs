using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.Event;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SOCIALNETWORK.WEB.Controllers
{
    [Authorize]
    [RoutePrefix("evento")]
    public class EventController : Controller
    {
        private readonly IEventHandler _eventHandler;
        private readonly IUserHandler _userHandler;

        public EventController(IEventHandler eventHandler,IUserHandler userHandler)
        {
            _eventHandler = eventHandler;
            _userHandler = userHandler;
        }

        [Route("nuevo")]
        [HttpPost]
        public async Task<ActionResult> NewEvent(NewEventViewModel model)
        {
            try
            {
                model.UserOwnerId = ClaimsExtension.GetCurrentUserId();
                var result = await _eventHandler.NewEventAsync(model);
                return Json(result);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("get-mis-eventos")]
        [HttpGet]
        public async Task<ActionResult> GetMyEvents()
        {
            try
            {
                return Json(await _eventHandler.GetMyEventToListAsync(ClaimsExtension.GetCurrentUserId()), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("get-eventos-perfil")]
        [HttpPost]

        public async Task<ActionResult> GetEventsProfile(Guid userId)
        {
            return Json(await _eventHandler.GetMyEventToListAsync(userId));
        }

        [Route("get-eventos")]
        [HttpGet]
        public async Task<ActionResult> GetEvents()
        {
            try
            {
                return Json(await _eventHandler.GetEventToListAsync(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("mis-eventos")]
        [HttpGet]
        public ActionResult Index()
            => View();

        [Route("{id}/detalle")]
        public async Task<ActionResult> Detail(Guid id)
        {
            var eventDetails = await _eventHandler.GetDetailsAsync(ClaimsExtension.GetCurrentUserId(), id);
            var model = new EventViewModel
            {
                CreatedAt = eventDetails.CreatedAt,
                Description = eventDetails.Description,
                EventType = eventDetails.EventType,
                Id = eventDetails.Id,
                Name = eventDetails.Name,
                UserOwnerName = eventDetails.UserOwnerName,
                PeopleToNeed = eventDetails.PeopleToNeed
            };

            return View(model);
        }

        [Route("{eventId}/unirse")]
        [HttpGet]
        public async Task<ActionResult> Join(Guid eventId)
        {
            var result = await _eventHandler.JoinAsync(ClaimsExtension.GetCurrentUserId(), eventId);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [Route("{eventId}/eliminar")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid eventId)
        {
            try
            {
                var userId = ClaimsExtension.GetCurrentUserId();
                return Json(await _eventHandler.DeleteAsync(userId, eventId), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpPost]
        [Route("{eventId}/participantes")]
        public async Task<ActionResult> GetMembers(Guid eventId)
        {
            var result = await _eventHandler.GetMembersAsync(eventId);
            return Json(result);
        }

        [Route("{eventId}/salir")]
        [HttpGet]
        public async Task<ActionResult> GoOut(Guid eventId)
        {
            try
            {
                var result = await _eventHandler.GoOutAsync(eventId, ClaimsExtension.GetCurrentUserId());
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("usuario/{userId}")]
        [HttpGet]
        public async Task<ActionResult> UserProfile(Guid userId)
        {
            var currentUserId = ClaimsExtension.GetCurrentUserId();
            if (userId == currentUserId)
                return RedirectToAction("Index", "User");

            var user = await _userHandler.GetProfileAsync(userId);
            var userModel = new ProfileViewModel
            {
                Id = user.Id,
                Degree = string.IsNullOrEmpty(user.Degree)?"-":user.Degree,
                DegreeId = user.DegreeId,
                Dni = string.IsNullOrEmpty(user.Dni)?"-":user.Dni,
                FacebookUrl = string.IsNullOrEmpty(user.FacebookUrl) ? "-" : user.Dni,
                LinkendlnUrl = string.IsNullOrEmpty(user.LinkendlnUrl) ? "-" : user.Dni,
                MatSurname = user.MatSurname,
                Name = user.Name,
                PatSurname = user.PatSurname,
                PhoneNumber = string.IsNullOrEmpty(user.PhoneNumber)?"-":user.PhoneNumber,
                StudyCenter = string.IsNullOrEmpty(user.StudyCenter)?"-":user.StudyCenter,
                StudyCenterId = user.StudyCenterId,
                TwitterUrl = string.IsNullOrEmpty(user.TwitterUrl) ? "-" : user.Dni,
            };

            return View(userModel);
        }
    }
}