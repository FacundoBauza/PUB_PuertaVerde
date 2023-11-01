using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccesLayer.Models
{
    [Table(name: "Producto_Ingrediente")]
    [PrimaryKey(nameof(Id_Producto), nameof(Id_Ingrediente))]
    public class Productos_Ingredientes
    {
        public int Id_Producto { get; set; }
        public Productos? Productos { get; set; }
        public int Id_Ingrediente { get; set; }
        public Ingredientes? Ingredientes { get; set; }

        public Productos_Ingredientes GetProductoIngredientes()
        {
            Productos_Ingredientes aux = new()
            {
                Id_Producto = Id_Producto,
                Id_Ingrediente = Id_Ingrediente
            };
            return aux;
        }
    }

}
/*
 public class Producto
{
    public int ProductoId { get; set; }
    public string Nombre { get; set; }
    // Other properties
    
    // Navigation property for the many-to-many relationship
    public ICollection<Productos_Ingredientes> ProductosIngredientes { get; set; }
}

public class Ingrediente
{
    public int IngredienteId { get; set; }
    public string Nombre { get; set; }
    // Other properties
    
    // Navigation property for the many-to-many relationship
    public ICollection<Productos_Ingredientes> ProductosIngredientes { get; set; }
}

public class Productos_Ingredientes
{
    public int ProductoId { get; set; }
    public int IngredienteId { get; set; }
    
    // Navigation properties for EF to understand the relationship
    public Producto Producto { get; set; }
    public Ingrediente Ingrediente { get; set; }
}

 */
