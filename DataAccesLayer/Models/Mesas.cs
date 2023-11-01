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
        public int Id_Mesa { get; set; }
        public bool EnUso { get; set; }
        public bool Registro_Activo { get; set; }
        public float PrecioTotal { get; set; }

        internal static Mesas SetMesa(DTMesa p)
        {
            Mesas aux = new()
            {
                Id_Mesa = p.id_Mesa,
                EnUso = p.enUso,
                PrecioTotal = p.precioTotal,
                Registro_Activo = true
            };
            return aux;
        }

        public Mesas GetMesa()
        {
            Mesas aux = new()
            {
                Id_Mesa = Id_Mesa,
                EnUso = EnUso,
                Registro_Activo = Registro_Activo,
                PrecioTotal = PrecioTotal
            };
            return aux;
        }
    }
}
