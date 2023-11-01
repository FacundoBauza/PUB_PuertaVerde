using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTCaja
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public float TotalPrecios { get; set; }
        public Boolean estado { get; set; }

        public DTCaja()
        {
        }

        public DTCaja(int id, DateTime fecha, float totalPrecios, bool estado)
        {
            this.id = id;
            this.fecha = fecha;
            TotalPrecios = totalPrecios;
            this.estado = estado;
        }
    }
}
