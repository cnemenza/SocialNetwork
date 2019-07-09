using SOCIALNETWORK.WEB.Models.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.WEB.Services.Handlers.Interfaces
{
    public interface IForumHandler
    {
        Task<MessagesViewModel[]> GetMessages(Guid eventId);
        Task<string> NewMessage(NewMessageViewModel model);
        Task<bool> HasAuthorization(Guid eventId, Guid userId);
    }
}
