using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.API.Models.User
{
    public class ProfilePictureModel
    {
        public byte[] Image { get; set; }
        public Guid UserId { get; set; }
    }
}