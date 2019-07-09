using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALNETWORK.ENTITIES.Models
{
    public class StudyCenter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public IEnumerable<Degree> Degrees { get; set; }
    }
}
