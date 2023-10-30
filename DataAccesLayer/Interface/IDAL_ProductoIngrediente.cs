using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interface
{
    public interface IDAL_ProductoIngrediente
    {
        public bool ProductoIngrediente(int productoId, int ingredienteId);

        List<Ingredientes> getIngredientesProducto(int idProducto);

        bool bajaProductoIngrediente(int id_Producto, int id_Ingrediente);
    }
}
