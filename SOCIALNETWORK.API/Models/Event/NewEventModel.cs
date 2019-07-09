using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.API.Models.Event
{
    public class NewEventModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserOwnerId { get; set; }
        public string PeopleToNeed { get; set; }
    }
}