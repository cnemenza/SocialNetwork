using SOCIALNETWORK.CORE;
using SOCIALNETWORK.WEB.Models.Login;
using SOCIALNETWORK.WEB.Services.Handlers.Interfaces;
using SOCIALNETWORK.WEB.Services.Handlers.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SOCIALNETWORK.WEB.Services.Handlers.Implementations
{
    public class LoginHandler : ILoginHandler
    {
        private readonly IApiHandler _apiHandler;

        public LoginHandler(IApiHandler apiHandler)
        {
            _apiHandler = apiHandler;
        }

        public async Task<LoginResult> LoginAsync(LoginViewModel model)
            => await _apiHandler.PostAsync<LoginViewModel, LoginResult>(model, ConstantsHelpers.Api.Login.LOGIN);

        public async Task<string> RegisterAsync(RegisterViewModel model)
            => await _apiHandler.PostAsync<RegisterViewModel, string>(model, ConstantsHelpers.Api.Login.REGISTER);
    }
}