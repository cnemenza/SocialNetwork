using SOCIALNETWORK.API.Models.Forum;
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
    [RoutePrefix("api/foro")]
    public class ForumController : ApiController
    {
        [HttpGet]
        [Route("get-mensajes")]
        public async Task<IHttpActionResult> GetMessages(Guid eventId)
        {
            using (var _context = new DatabaseContext())
            {
                var messages = await _context.Forums.Where(x => x.EventId == eventId)
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(x => new
                    {
                        x.Message,
                        x.UserId,
                        x.User.UserDetail.Name,
                        x.User.UserDetail.PatSurname,
                        x.User.CreatedAt
                    })
                    .ToArrayAsync();

                return Ok(messages);
            }
        }

        [HttpPost]
        [Route("nuevo-mensaje")]
        public async Task<IHttpActionResult> NewMessage(NewMessageModel model)
        {
            using (var _context = new DatabaseContext())
            {
                var forum = new Forum
                {
                    CreatedAt = DateTime.Now,
                    EventId = model.EventId,
                    UserId = model.UserId,
                    Message = model.Message
                };

                _context.Forums.Add(forum);
                await _context.SaveChangesAsync();

                return Ok("Mensaje agregado.");
            }
        }

        [HttpGet]
        [Route("autorizacion")]
        public async Task<IHttpActionResult> HasAuthorization(Guid eventId,Guid userId)
        {
            using (var _context = new DatabaseContext())
            {
                var userIsRegister = await _context.Events.Where(x => x.Id == eventId).AnyAsync(x => x.Users.Any(y => y.Id == userId) || x.UserOwnerId == userId);
                return Ok(userIsRegister);
            }
        }
    }
}