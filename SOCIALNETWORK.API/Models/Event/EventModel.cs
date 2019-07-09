using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.API.Models.Event
{
    public class EventModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UserOwnerName { get; set; }
        public string PeopleToNeed { get; set; }
        public string CreatedAt { get; set; }
        public string EventType { get; set; }
    }
}