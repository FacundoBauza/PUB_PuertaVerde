using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccesLayer.Models
{
    [Table(name: "Producto_Ingrediente")]
    [PrimaryKey(nameof(id_Producto), nameof(id_Ingrediente))]
    public class Productos_Ingredientes
    {
        public int id_Producto { get; set; }
        public Productos? productos { get; set; }
        public int id_Ingrediente { get; set; }
        public Ingredientes? ingredientes { get; set; }

        public static Productos_Ingredientes SetProductos_Ingredientes(int idProducto, int idIngrediente)
        {
            Productos_Ingredientes aux = new Productos_Ingredientes();
            
            aux.id_Producto = idProducto;
            aux.id_Ingrediente = idIngrediente;

            return aux;
        }

        public Productos_Ingredientes GetProductoIngredientes()
        {
            Productos_Ingredientes aux = new()
            {
                id_Producto = id_Producto,
                id_Ingrediente = id_Ingrediente
            };
            return aux;
        }
    }

}
