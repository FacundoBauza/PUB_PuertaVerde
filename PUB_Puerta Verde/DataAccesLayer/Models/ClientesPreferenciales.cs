using Domain.DT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{

    [Table(name: "Cliente_Preferencial")]
    public class ClientesPreferenciales
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_Cli_Preferencial { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string telefono { get; set; }
        public float saldo { get; set; }
        public int fichasCanje { get; set; }
        public bool registro_Activo { get; set; }

        public static ClientesPreferenciales SetCliente(DTCliente_Preferencial x)
        {
            ClientesPreferenciales aux = new ClientesPreferenciales();

            aux.id_Cli_Preferencial = x.id_Cli_Preferencial;
            aux.nombre = x.nombre;
            aux.apellido = x.apellido;
            aux.telefono = x.telefono;
            aux.saldo = x.saldo;
            aux.fichasCanje = x.fichasCanje;
            aux.registro_Activo = true;

            return aux;
        }

        public ClientesPreferenciales GetCliente()
        {
            ClientesPreferenciales aux = new ClientesPreferenciales();

            aux.id_Cli_Preferencial = id_Cli_Preferencial;
            aux.nombre = nombre;
            aux.apellido = apellido;
            aux.telefono = telefono;
            aux.saldo = saldo;
            aux.fichasCanje = fichasCanje;
            aux.registro_Activo = registro_Activo;

            return aux;
        }

    }
}
