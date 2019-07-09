using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.ENTITIES.Models
{
    public class Event
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UserOwnerId { get; set; }
        public string PeopleToNeed { get; set; }

        public ICollection<Staff> Staffs { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<EventImage> EventImages { get; set; }
    }
}
