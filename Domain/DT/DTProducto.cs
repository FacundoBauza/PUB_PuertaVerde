using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTProducto
    {
        public int id_Producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public float precio { get; set; }

#pragma warning disable CS8618
        public DTProducto()
        {
        }

        public DTProducto(int id_Producto, string nombre, string descripcion, float precio)
        {
            this.id_Producto = id_Producto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
        }
    }
}
