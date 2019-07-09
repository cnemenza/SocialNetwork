using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SOCIALNETWORK.WEB.Models.Login
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="El campo 'Nombre' es requerido.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo 'Apellido Paterno' es requerido.")]
        public string PatSurname { get; set; }
        [Required(ErrorMessage = "El campo 'Correo Electronico' es requerido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo 'Contraseña' es requerido.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "El campo 'Verificar contraseña' es requerido.")]
        public string PasswordVerifier { get; set; }
    }
}