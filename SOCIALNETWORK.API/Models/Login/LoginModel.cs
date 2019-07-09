using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.API.Models.Login
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}