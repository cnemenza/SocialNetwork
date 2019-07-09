using SOCIALNETWORK.CORE;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALNETWORK.ENTITIES.Models
{
    public class User 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; } = ConstantsHelpers.User.Status.ACTIVE;
        public bool ConfirmedEmail { get; set; } = false;

        public Guid? UserDetailId { get; set; }
        public UserDetail UserDetail { get; set; }

        public ICollection<UserImage> UserImages { get; set; }
        public ICollection<Event> Events { get; set; }


    }
}
