using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.Event;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.WEB.Services.Handlers.Implementations
{
    public class EventHandler : IEventHandler
    {
        private readonly IApiHandler _apiHandler;

        public EventHandler(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public async Task<string> NewEventAsync(NewEventViewModel model)
            => await _apiHandler.PostAsync<NewEventViewModel, string>(model, ConstantsHelpers.Api.Event.NEWEVENT);

        public async Task<EventToListViewModel[]> GetEventToListAsync()
            => await _apiHandler.GetAsync<EventToListViewModel[]>(ConstantsHelpers.Api.Event.GETEVENTS);

        public async Task<EventToListViewModel[]> GetMyEventToListAsync(Guid userId)
            => await _apiHandler.GetAsync<EventToListViewModel[]>(String.Format(ConstantsHelpers.Api.Event.GETMYEVENTS, userId));

        public async Task<EventViewModel> GetDetailsAsync(Guid userId, Guid eventId)
            => await _apiHandler.GetAsync<EventViewModel>(String.Format(ConstantsHelpers.Api.Event.GETDETAILS, eventId, userId));

        public async Task<string> JoinAsync(Guid userId, Guid eventId)
            => await _apiHandler.GetAsync<string>(String.Format(ConstantsHelpers.Api.Event.JOIN,eventId,userId));

        public async Task<string> DeleteAsync(Guid userId, Guid eventId)
            => await _apiHandler.GetAsync<string>(String.Format(ConstantsHelpers.Api.Event.DELETE, eventId, userId));

        public async Task<MembersToListViewModel[]> GetMembersAsync(Guid eventId)
            => await _apiHandler.GetAsync<MembersToListViewModel[]>(String.Format(ConstantsHelpers.Api.Event.GETMEMBERS, eventId));

        public async Task<string> GoOutAsync(Guid eventId, Guid userId)
            => await _apiHandler.GetAsync<string>(String.Format(ConstantsHelpers.Api.Event.GOOUT,eventId,userId));

    }
}