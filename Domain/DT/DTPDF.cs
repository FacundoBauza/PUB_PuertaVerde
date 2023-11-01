using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTPDF
    {
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }

        public DTPDF()
        {
        }

        public DTPDF(string nombre, int cantidad, float precio)
        {
            this.nombre = nombre;
            this.cantidad = cantidad;
            this.precio = precio;
        }
    }
}
