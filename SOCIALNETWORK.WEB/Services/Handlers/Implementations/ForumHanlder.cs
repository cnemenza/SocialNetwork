using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.Forum;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.WEB.Services.Handlers.Implementations
{
    public class ForumHanlder : IForumHandler
    {
        private readonly IApiHandler _apiHandler;

        public ForumHanlder(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public async Task<MessagesViewModel[]> GetMessages(Guid eventId)
            => await _apiHandler.GetAsync<MessagesViewModel[]>(String.Format(ConstantsHelpers.Api.Forum.GETMESSAGES, eventId));

        public async Task<bool> HasAuthorization(Guid eventId, Guid userId)
            => await _apiHandler.GetAsync<bool>(String.Format(ConstantsHelpers.Api.Forum.HASAUTHORIZATION, eventId, userId));

        public async Task<string> NewMessage(NewMessageViewModel model)
            => await _apiHandler.PostAsync<NewMessageViewModel, string>(model, ConstantsHelpers.Api.Forum.NEWMESSAGE);
    }
}