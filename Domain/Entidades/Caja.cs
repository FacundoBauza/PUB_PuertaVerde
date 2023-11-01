using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Caja
    {
        public int id;
        public DateTime fecha;
        public float TotalPrecios;
        public Boolean estado;

        public Caja()
        {
        }

        public Caja(int id, DateTime fecha, float totalPrecios, bool estado)
        {
            this.id = id;
            this.fecha = fecha;
            TotalPrecios = totalPrecios;
            this.estado = estado;
        }
    }
}
