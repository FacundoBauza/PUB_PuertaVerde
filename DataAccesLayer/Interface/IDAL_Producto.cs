using DataAccesLayer.Models;
using Domain.DT;
using Domain.Enums;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Producto
    {
        public List<Productos> getProducto();
        List<Productos> getProductoPorTipo(Categoria tipo);
        bool modificar_Producto(DTProducto dtp);
        public int set_Producto(DTProducto dtp);
    }
}
