using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SOCIALNETWORK.ENTITIES.Models
{
    public class UserDetail 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string PatSurname { get; set; }
        public string MatSurname { get; set; }
        public string Dni { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkedlnUrl { get; set; }
        public bool NeedtoUpdate { get; set; } = true;

        public Guid? StudyCenterId { get; set; }
        public StudyCenter StudyCenter { get; set; }

        public Guid? DegreeId { get; set; }
        public Degree Degree { get; set; }
    }
}
