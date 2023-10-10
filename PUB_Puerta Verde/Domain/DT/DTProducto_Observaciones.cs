using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTProducto_Observaciones
    {
        public int id_Producto { get; set; }
        public string observaciones { get; set; }

#pragma warning disable CS8618
        public DTProducto_Observaciones()
        {
        }

        public DTProducto_Observaciones(int id_Producto, string observaciones)
        {
            this.id_Producto = id_Producto;
            this.observaciones = observaciones;
        }
    }
}
