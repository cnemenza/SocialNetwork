using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.WEB.Services.Handlers.Models.Login
{
    public class LoginResult
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PatSurname { get; set; }
        public string Email { get; set; }
    }
}