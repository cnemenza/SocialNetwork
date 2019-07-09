using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.WEB.Models.Forum
{
    public class MessagesViewModel
    {
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string PatSurname { get; set; }
        public string CreatedAt { get; set; }
    }
}