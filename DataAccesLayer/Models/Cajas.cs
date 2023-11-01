using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    public class Cajas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id;
        public DateTime fecha;
        public float TotalPrecios;
        public Boolean estado;

        public Cajas()
        {
        }

        public Cajas(int id, DateTime fecha, float totalPrecios, bool estado)
        {
            this.id = id;
            this.fecha = fecha;
            TotalPrecios = totalPrecios;
            this.estado = estado;
        }
    }
}
