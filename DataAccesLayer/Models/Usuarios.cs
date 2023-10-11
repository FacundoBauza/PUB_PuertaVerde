using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class Usuarios: IdentityUser
    {

        [MaxLength(128), MinLength(3), Required]
        public string? nombre { get; set; }
        [MaxLength(128), MinLength(3), Required]
        public string? apellido { get; set; }
        public bool registro_Activo { get; set; }
    }
}
