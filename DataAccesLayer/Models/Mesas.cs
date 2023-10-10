using Domain.DT;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccesLayer.Models
{
    [Table(name: "Mesa")]
    public class Mesas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_Mesa { get; set; }
        public bool enUso { get; set; }
        public bool registro_Activo { get; set; }
        public float precioTotal { get; set; }

        internal static Mesas SetMesa(DTMesa p)
        {
            Mesas aux = new Mesas();
            aux.id_Mesa = p.id_Mesa;
            aux.enUso = p.enUso;
            aux.precioTotal = p.precioTotal;
            aux.registro_Activo = true;
            return aux;
        }

        public Mesas GetMesa()
        {
            Mesas aux = new Mesas();
            aux.id_Mesa = id_Mesa;
            aux.enUso = enUso;
            aux.registro_Activo = registro_Activo;
            aux.precioTotal = precioTotal;
            return aux;
        }
    }
}
