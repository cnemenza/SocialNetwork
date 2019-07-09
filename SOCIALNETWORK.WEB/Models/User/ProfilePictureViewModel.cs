using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.WEB.Models.User
{
    public class ProfilePictureViewModel
    {
        public byte[] Image { get; set; }
        public Guid UserId { get; set; }
    }
}