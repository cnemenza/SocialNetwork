using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALNETWORK.ENTITIES.Models
{
    public class Degree
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public Guid StudyCenterId { get; set; }
        public StudyCenter StudyCenter { get; set; }
    }
}
