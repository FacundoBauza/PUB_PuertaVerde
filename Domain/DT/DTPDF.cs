using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTPDF
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public float Precio { get; set; }

        public DTPDF(string nombre, int cantidad, float precio)
        {
            this.Nombre = nombre;
            this.Cantidad = cantidad;
            this.Precio = precio;
        }
    }
}
