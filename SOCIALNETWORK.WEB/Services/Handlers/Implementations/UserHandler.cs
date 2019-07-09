using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.User;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.WEB.Services.Handlers.Implementations
{
    public class UserHandler : IUserHandler
    {
        private readonly IApiHandler _apiHandler;

        public UserHandler(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public async Task<string> UpdateProfileAsync(ProfileViewModel model)
            => await _apiHandler.PostAsync<ProfileViewModel, string>(model, ConstantsHelpers.Api.User.UPDATEPROFILE);

        public async Task<ProfileViewModel> GetProfileAsync(Guid userId)
            => await _apiHandler.GetAsync<ProfileViewModel>(String.Format(ConstantsHelpers.Api.User.GETPROFILE,userId));

        public async Task<string> UpdatePhotoAsync(ProfilePictureViewModel model)
            => await _apiHandler.PostAsync<ProfilePictureViewModel, string>(model, ConstantsHelpers.Api.User.UPDATEPHOTO);
    }
}