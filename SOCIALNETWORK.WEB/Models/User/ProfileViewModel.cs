using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.WEB.Models.User
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PatSurname { get; set; }
        public string MatSurname { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string LinkendlnUrl { get; set; }
        public Guid? StudyCenterId { get; set; }
        public Guid? DegreeId { get; set; }
        public string StudyCenter { get; set; }
        public string Degree { get; set; }
    }
}