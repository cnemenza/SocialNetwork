using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALNETWORK.ENTITIES.Models
{
    public class UserImage 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        public byte[] Image { get; set; }
    }
}
