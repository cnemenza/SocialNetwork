using SOCIALNETWORK.API.Models.User;
using SOCIALNETWORK.ENTITIES.Models;
using SOCIALNETWORK.REPOSITORY.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace SOCIALNETWORK.API.Controllers
{
    [RoutePrefix("api/usuario")]
    public class UserController : ApiController
    {
        [HttpPost]
        [Route("actualizar-perfil")]
        public async Task<IHttpActionResult> UpdateProfile(ProfileModel model)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var user = await _context.Users.Where(x => x.Id == model.Id).FirstOrDefaultAsync();
                    var userDetail = await _context.UserDetails.Where(x => x.Id == user.UserDetailId).FirstOrDefaultAsync();

                    userDetail.NeedtoUpdate = false;
                    userDetail.Name = model.Name;
                    userDetail.PatSurname = model.PatSurname;
                    userDetail.MatSurname = string.IsNullOrEmpty(model.MatSurname)?null:model.MatSurname;
                    userDetail.Dni = model.Dni;
                    userDetail.PhoneNumber = model.PhoneNumber;
                    userDetail.FacebookUrl = string.IsNullOrEmpty(model.FacebookUrl) ? null : model.FacebookUrl;
                    userDetail.TwitterUrl = string.IsNullOrEmpty(model.TwitterUrl) ? null : model.TwitterUrl;
                    userDetail.LinkedlnUrl = string.IsNullOrEmpty(model.LinkendlnUrl) ? null : model.LinkendlnUrl;
                    userDetail.StudyCenterId = model.StudyCenterId;
                    userDetail.DegreeId = model.DegreeId;

                    await _context.SaveChangesAsync();

                    return Ok("Los datos han sido actualizados.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Error al actualizar el perfil.");
            }
            
        }

        [HttpGet]
        [Route("get-perfil")]
        public async Task<IHttpActionResult> GetProfile(Guid userId)
        {
            using (var _context = new DatabaseContext())
            {
                try
                {
                    var userDetailId = await _context.Users.Where(x => x.Id == userId).Select(x => x.UserDetailId).FirstOrDefaultAsync();
                    var userDetail = await _context.UserDetails.Where(x => x.Id == userDetailId).Include(x => x.StudyCenter).Include(x => x.Degree).FirstOrDefaultAsync();

                    var model = new ProfileModel
                    {
                        Id = userId,
                        Name = userDetail.Name,
                        PatSurname = userDetail.PatSurname,
                        MatSurname = userDetail.MatSurname,
                        Dni = userDetail.Dni,
                        PhoneNumber = userDetail.PhoneNumber,
                        FacebookUrl = userDetail.FacebookUrl,
                        TwitterUrl = userDetail.TwitterUrl,
                        LinkendlnUrl = userDetail.LinkedlnUrl,
                        StudyCenterId = userDetail.StudyCenterId is null ? null : userDetail.StudyCenterId,
                        StudyCenter = userDetail.StudyCenterId is null ? null : userDetail.StudyCenter.Name,
                        DegreeId = userDetail.DegreeId is null ? null : userDetail.DegreeId,
                        Degree = userDetail.DegreeId is null ? null : userDetail.Degree.Name
                    };

                    return Ok(model);
                }
                catch (Exception ex)
                {

                    throw;
                }
      
            }
        }

        [HttpPost]
        [Route("actualizar-foto")]
        public async Task<IHttpActionResult> UpdatePhoto(ProfilePictureModel model)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var hasPhotos = await _context.UserImages.AnyAsync(x => x.UserId == model.UserId);
                    if (hasPhotos)
                    {
                        var userImages = await _context.UserImages.Where(x => x.UserId == model.UserId).ToListAsync();
                        _context.UserImages.RemoveRange(userImages);
                        await _context.SaveChangesAsync();
                    }

                    var userImage = new UserImage
                    {
                        UserId = model.UserId,
                        Image = model.Image
                    };

                    _context.UserImages.Add(userImage);
                    await _context.SaveChangesAsync();
                }

                return Ok("La foto ha sido actualizada correctamente.");
            }
            catch (Exception)
            {

                return BadRequest("Error al actualizar la foto");
            }
           
        }

    }
}