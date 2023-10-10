using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTUsuario
    {
        public string? Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public DateTime FechaHora { get; set; }
        public bool registro_Activo { get; set; }
        public string[] Roles { get; set; } = { };
    }
}
