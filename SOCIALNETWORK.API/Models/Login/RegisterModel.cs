using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.API.Models.Login
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string PatSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordVerifier { get; set; }
    }
}