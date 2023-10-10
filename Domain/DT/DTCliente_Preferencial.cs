using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTCliente_Preferencial
    {
        public int id_Cli_Preferencial { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public float saldo { get; set; }
        public bool registro_Activo { get; set; }
        public int fichasCanje { get; set; }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public DTCliente_Preferencial()
        {
        }

        public DTCliente_Preferencial(int id_Cli_Preferencial, string nombre, string apellido, string telefono, float saldo, bool registro_Activo, int fichasCanje)
        {
            this.id_Cli_Preferencial = id_Cli_Preferencial;
            this.nombre = nombre;
            this.apellido = apellido;
            this.telefono = telefono;
            this.saldo = saldo;
            this.registro_Activo = registro_Activo;
            this.fichasCanje = fichasCanje;
        }
    }
}
