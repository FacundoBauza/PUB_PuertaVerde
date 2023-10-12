using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    [Table(name: "Producto_Ingrediente")]
    [PrimaryKey(nameof(id_Producto), nameof(id_Ingrediente))]
    public class Productos_Ingredientes
    {
        public int id_Producto { get; set; }
        public Productos Productos { get; set; }
        public int id_Ingrediente { get; set; }
        public Ingredientes Ingredientes { get; set; }
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
