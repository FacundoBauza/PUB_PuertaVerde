using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DT;

namespace DataAccesLayer.Models
{
    [Table(name: "Cajas")]
    public class Cajas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }
        
        public float TotalPrecios { get; set; }
        
        public Boolean Estado { get; set; }

        public Cajas GetCajas()
        {
            Cajas aux = new ()
            {
                Id = Id,
                Fecha = Fecha,
                TotalPrecios = TotalPrecios,
                Estado = Estado
            };
            return aux;
        }
        internal static Cajas SetCajas(DTCaja p)
        {
            Cajas aux = new()
            {
                Id = p.id,
                Fecha=p.fecha,
                TotalPrecios = p.TotalPrecios,
                Estado = p.estado
            };
            return aux;
        }
    }
}
