using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Producto
    {
        public List<Productos> getProducto();
        bool modificar_Producto(DTProducto dtp);
        public bool set_Producto(DTProducto dtp);
    }
}
