using SOCIALNETWORK.API.Models.Event;
using SOCIALNETWORK.CORE;
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
    [RoutePrefix("api/evento")]
    public class EventController : ApiController
    {
        [HttpPost]
        [Route("nuevo")]
        public async Task<IHttpActionResult> NewEvent(NewEventModel model)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var newEvent = new Event
                    {
                        Id = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        Description = model.Description,
                        Name = model.Name,
                        UserOwnerId = model.UserOwnerId,
                        PeopleToNeed = model.PeopleToNeed
                    };

                    _context.Events.Add(newEvent);
                    await _context.SaveChangesAsync();
                }

                return Ok("Evento registrado con éxito.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear el evento");
            }
        }

        [HttpGet]
        [Route("get-mis-eventos")]
        public async Task<IHttpActionResult> GetMyEvents(Guid userId)
        {
            using (var _context = new DatabaseContext())
            {
                var events = await _context.Events.Where(x => x.UserOwnerId == userId
                    || x.Users.Any(y => y.Id == userId))
                    .OrderByDescending(x => x.CreatedAt)
                    .Select(
                    x => new EventToListModel
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Name = x.Name,
                        UserOwnerName = x.UserOwnerId.ToString()
                    }).ToArrayAsync();

                foreach (var item in events)
                {
                    var userOwnerId = Guid.Parse(item.UserOwnerName);
                    var user = await _context.Users.Where(x => x.Id == userOwnerId)
                        .Select(x => new { x.UserDetail.Name, x.UserDetail.PatSurname }).FirstOrDefaultAsync();
                    item.UserOwnerName = $"{user.Name} {user.PatSurname}";
                }

                return Ok(events);
            }
        }

        [HttpGet]
        [Route("get-eventos")]
        public async Task<IHttpActionResult> GetEvents()
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var events = await _context.Events
                        .OrderByDescending(x => x.CreatedAt)
                        .Select(
                        x => new EventToListModel
                        {
                            Id = x.Id,
                            Description = x.Description,
                            Name = x.Name,
                            UserOwnerName = x.UserOwnerId.ToString()
                        }).ToArrayAsync();

                    foreach (var item in events)
                    {
                        var userId = Guid.Parse(item.UserOwnerName);
                        var user = await _context.Users.Where(x => x.Id == userId).Select(x => new { x.UserDetail.Name, x.UserDetail.PatSurname }).FirstOrDefaultAsync();
                        item.UserOwnerName = $"{user.Name} {user.PatSurname}";
                    }

                    return Ok(events);
                }
            }
            catch (Exception ex)
            {

                return BadRequest("");
            }

        }

        [HttpGet]
        [Route("{id}/detalle/{userId}")]
        public async Task<IHttpActionResult> GetDetails(Guid id, Guid userId)
        {
            using (var _context = new DatabaseContext())
            {
                var eventDetails = await _context.Events.Where(x => x.Id == id).Include(x => x.Users).FirstOrDefaultAsync();
                var model = new EventModel
                {
                    CreatedAt = $"{eventDetails.CreatedAt:dd-MM-yyyy}",
                    Description = eventDetails.Description,
                    Id = eventDetails.Id,
                    Name = eventDetails.Name,
                    PeopleToNeed = eventDetails.PeopleToNeed
                };

                var user = await _context.Users.Where(x => x.Id == eventDetails.UserOwnerId).Select(x => new { x.UserDetail.Name, x.UserDetail.PatSurname }).FirstOrDefaultAsync();
                model.UserOwnerName = $"{user.Name} {user.PatSurname}";

                if (eventDetails.UserOwnerId == userId)
                {
                    model.EventType = ConstantsHelpers.Event.Type.ISOWNER;
                    return Ok(model);
                }

                var userIsRegister = await _context.Events.Where(x=>x.Id==id).AnyAsync(x => x.Users.Any(y => y.Id == userId));

                if (userIsRegister)
                {
                    model.EventType = ConstantsHelpers.Event.Type.ISUSERREGISTER;
                }
                else
                {
                    model.EventType = ConstantsHelpers.Event.Type.ISUSERFREE;
                }

                return Ok(model);
            }
        }


        [HttpGet]
        [Route("{eventId}/unirse/{userId}")]
        public async Task<IHttpActionResult> Join(Guid eventId, Guid userId)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var eventToEdit = await _context.Events.Where(x => x.Id == eventId).Include(x => x.Users).FirstOrDefaultAsync();
                    var user = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
                    eventToEdit.Users.Add(user);

                    await _context.SaveChangesAsync();

                    return Ok("Te has unido con éxito.");
                }
            }
            catch (Exception)
            {
                return BadRequest("Error al unirse al evento.");
            }

        }

        [HttpGet]
        [Route("{eventId}/eliminar/{userId}")]
        public async Task<IHttpActionResult> Delete(Guid eventId,Guid userId)
        {
            try
            {
                using(var _context = new DatabaseContext())
                {
                    var eventToDelete = await _context.Events.Where(x => x.Id == eventId).Include(x=>x.Users).FirstOrDefaultAsync();
                    if(eventToDelete != null)
                    {
                        if (eventToDelete.UserOwnerId != userId)
                            return BadRequest("Solo el dueño del evento puede eliminarlo.");

                        eventToDelete.Users.Clear();
                        _context.Events.Remove(eventToDelete);
                        await _context.SaveChangesAsync();
                    }
                }

                return Ok("Evento eliminado con exito.");
            }
            catch (Exception)
            {

                return BadRequest("Error al eliminar el evento.");
            }
        }

        [HttpGet]
        [Route("{eventId}/participantes")]
        public async Task<IHttpActionResult> GetMembers(Guid eventId)
        {
            try
            {
                using (var _context = new DatabaseContext())
                {
                    var members = await _context.Users.Where(x => x.Events.Any(y => y.Id == eventId))
                        .Select(x => new MembersToListModel
                        {
                            Id = x.Id,
                            Email = x.Email,
                            Name = x.UserDetail.Name,
                            PatSurname = x.UserDetail.PatSurname
                        })
                        .ToListAsync();

                    var userOwnerId = await _context.Events.Where(x => x.Id == eventId).Select(x => x.UserOwnerId).FirstOrDefaultAsync();
                    var member = await _context.Users.Where(x => x.Id == userOwnerId)
                        .Select(x => new
                        {
                            x.Id,
                            x.Email,
                            x.UserDetail.Name,
                            x.UserDetail.PatSurname
                        })
                        .FirstOrDefaultAsync();

                    members.Add(new MembersToListModel
                    {
                        Id = member.Id,
                        Email = member.Email,
                        Name = member.Name,
                        PatSurname = member.PatSurname
                    });

                    return Ok(members);
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Error al cargar los participantes.");
            }
            
        }

        [HttpGet]
        [Route("{eventId}/salir/{userId}")]
        public async Task<IHttpActionResult> GoOut(Guid eventId,Guid userId)
        {
            using(var _context = new DatabaseContext())
            {
                try
                {
                    var Event = await _context.Events.Where(x => x.Id == eventId).Include(x=>x.Users).FirstOrDefaultAsync();
                    var userToDelete = await _context.Users.Where(x => x.Id == userId).FirstOrDefaultAsync();
                    Event.Users.Remove(userToDelete);
                    await _context.SaveChangesAsync();
                    return Ok("Usted ha salido correctamente del evento.");
                }
                catch (Exception ex)
                {
                    return BadRequest("Error al eliminar el evento.");
                }

            }
        }
    }
}