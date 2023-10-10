using System.ComponentModel.DataAnnotations;

namespace WebApi_PUB_PV.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El nombre de Usuario es requerido")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string? Password { get; set; }
    }
}
