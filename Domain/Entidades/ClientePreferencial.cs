using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class ClientePreferencial
    {
        public int id_Cli_Preferencial { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public int fichasCanje { get; set; }
        public float saldo { get; set; }
        public bool registro_Activo { get; set; }

#pragma warning disable CS8618
        public ClientePreferencial()
        {
        }

        public ClientePreferencial(int id_Cli_Preferencial, string nombre, string apellido, string telefono,  float saldo, int fichasCanje, bool registro_Activo)
        {
            this.id_Cli_Preferencial = id_Cli_Preferencial;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.saldo = saldo;
            this.fichasCanje = fichasCanje;
            this.registro_Activo = registro_Activo;
        }
    }
}
