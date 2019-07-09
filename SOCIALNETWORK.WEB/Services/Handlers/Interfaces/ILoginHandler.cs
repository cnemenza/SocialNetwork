using SOCIALNETWORK.WEB.Models.Login;
using SOCIALNETWORK.WEB.Services.Handlers.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.WEB.Services.Handlers.Interfaces
{
    public interface ILoginHandler
    {
        Task<LoginResult> LoginAsync(LoginViewModel model);
        Task<string> RegisterAsync(RegisterViewModel model);
    }
}
