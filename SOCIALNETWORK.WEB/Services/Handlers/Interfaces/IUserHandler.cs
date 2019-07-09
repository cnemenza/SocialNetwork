using SOCIALNETWORK.WEB.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.WEB.Services.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task<string> UpdateProfileAsync(ProfileViewModel model);
        Task<ProfileViewModel> GetProfileAsync(Guid userId);
        Task<string> UpdatePhotoAsync(ProfilePictureViewModel model);
    }
}
