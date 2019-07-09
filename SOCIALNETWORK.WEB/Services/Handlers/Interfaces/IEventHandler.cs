using SOCIALNETWORK.WEB.Models.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.WEB.Services.Handlers.Interfaces
{
    public interface IEventHandler
    {
        Task<string> NewEventAsync(NewEventViewModel model);
        Task<EventToListViewModel[]> GetEventToListAsync();
        Task<EventToListViewModel[]> GetMyEventToListAsync(Guid userId);
        Task<EventViewModel> GetDetailsAsync(Guid userId, Guid eventId);
        Task<string> JoinAsync(Guid userId, Guid eventId);
        Task<string> DeleteAsync(Guid userId, Guid eventId);
        Task<MembersToListViewModel[]> GetMembersAsync(Guid eventId);
        Task<string> GoOutAsync(Guid eventId,Guid userId);
    }
}
