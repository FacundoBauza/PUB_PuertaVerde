using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTProductoEstadistica
    {
        public int Cantidad { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fin { get; set; }
        public DTProducto? Producto { get; set; }
    }
}
