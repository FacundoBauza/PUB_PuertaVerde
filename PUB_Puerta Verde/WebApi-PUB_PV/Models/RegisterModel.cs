using System.ComponentModel.DataAnnotations;

namespace WebApi_PUB_PV.Models
{
    public class RegisterModel
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        [Required(ErrorMessage = "El nombre es requerido")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string? Apellido { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
    }
}
