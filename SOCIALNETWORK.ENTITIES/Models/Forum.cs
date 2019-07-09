using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOCIALNETWORK.ENTITIES.Models
{
    public class Forum
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
