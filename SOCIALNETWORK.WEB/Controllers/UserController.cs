using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.User;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SOCIALNETWORK.WEB.Controllers
{
    [Authorize]
    [RoutePrefix("perfil")]
    public class UserController : Controller
    {
        private readonly IStudyCenterHandler _studyCenterHandler;
        private readonly IUserHandler _userHandler;

        public UserController(IStudyCenterHandler studyCenterHandler,IUserHandler userHandler)
        {
            _studyCenterHandler = studyCenterHandler;
            _userHandler = userHandler;
        }

        [Route("")]
        public async Task<ActionResult> Index()
        {
            var userId = ClaimsExtension.GetCurrentUserId();
            var profile = await _userHandler.GetProfileAsync(userId);
            var studyCenters = await _studyCenterHandler.GetStudyCentersToListAsync();

            if(profile.StudyCenterId != null)
            {
                ViewBag.degrees = await _studyCenterHandler.GetDegreesByStudyCenterToListAsync((Guid)profile.StudyCenterId);
            }

            ViewBag.studyCenters = studyCenters;
            var model = new ProfileViewModel
            {
                DegreeId = profile.DegreeId,
                Dni = profile.Dni,
                FacebookUrl = profile.FacebookUrl,
                Id = profile.Id,
                LinkendlnUrl = profile.LinkendlnUrl,
                MatSurname = profile.MatSurname,
                Name = profile.Name,
                PatSurname = profile.PatSurname,
                PhoneNumber = profile.PhoneNumber,
                StudyCenterId = profile.StudyCenterId,
                TwitterUrl = profile.TwitterUrl
            };

            return View(model);
        }

        [Route("listar-carreras")]
        [HttpGet]
        public async Task<ActionResult> GetDegreesByStudyCenterId(Guid studyCenterId)
            => Json(await _studyCenterHandler.GetDegreesByStudyCenterToListAsync(studyCenterId), JsonRequestBehavior.AllowGet);

        [Route("actualizar-perfil")]
        [HttpPost]
        public async Task<ActionResult> UpdateProfile(ProfileViewModel model)
        {
            var splitName = model.Name.Split(' ').Where(x=>!string.IsNullOrEmpty(x)).ToArray();
            if (splitName.Count() == 1)
                model.Name = splitName[0];

            if(splitName.Count() == 2)
            {
                model.Name = splitName[0];
                model.PatSurname = splitName[1];
            }

            if(splitName.Count() >= 3)
            {
                model.Name = splitName[0];
                model.PatSurname = splitName[1];
                model.MatSurname = splitName[2];
            }

            return Json(await _userHandler.UpdateProfileAsync(model));
        }

        [Route("actualizar-foto")]
        [HttpPost]
        public async Task<ActionResult> UpdatePhoto(HttpPostedFileBase file)
        {
            try
            {
                var imageArray = new byte[file.ContentLength];
                file.InputStream.Read(imageArray, 0, file.ContentLength);
                var model = new ProfilePictureViewModel
                {
                    Image = imageArray,
                    UserId = ClaimsExtension.GetCurrentUserId()
                };

                var result = await _userHandler.UpdatePhotoAsync(model);

                return Json(result);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}