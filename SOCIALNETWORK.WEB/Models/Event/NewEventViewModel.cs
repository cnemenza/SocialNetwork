using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.WEB.Models.Event
{
    public class NewEventViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserOwnerId { get; set; }
        public string PeopleToNeed { get; set; }
    }
}