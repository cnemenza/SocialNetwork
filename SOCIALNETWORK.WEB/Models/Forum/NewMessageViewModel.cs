using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.WEB.Models.Forum
{
    public class NewMessageViewModel
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
        public string Message { get; set; }
    }
}