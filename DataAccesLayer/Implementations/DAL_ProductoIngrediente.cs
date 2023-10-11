using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_ProductoIngrediente : IDAL_ProductoIngrediente
    {
        private readonly DataContext context;

        public DAL_ProductoIngrediente(DataContext db)
        {
            context = db;
        }
        public bool ProductoIngrediente(int productoId , int ingredienteId)
        {
            // Busca el producto y el ingrediente existentes por sus IDs
            var producto = context.Productos.FirstOrDefault(p => p.id_Producto == productoId); 
            var ingrediente = context.Ingredientes.FirstOrDefault(i => i.id_Ingrediente == ingredienteId);

            // Verifica que ambos existan en la base de datos
            if (producto != null && ingrediente != null)
            {
                // Crea una instancia de ProductoIngrediente y establece las relaciones
                Productos_Ingredientes productoIngrediente = new();
                productoIngrediente.Productos = producto;
                productoIngrediente.Ingredientes = ingrediente;
                productoIngrediente.id_Producto = productoId;
                productoIngrediente.id_Ingrediente = ingredienteId;
                // Agrega el productoIngrediente a la tabla intermedia
                context.Productos_Ingredientes.Add(productoIngrediente);

                // Guarda los cambios en la base de datos
                context.SaveChanges();

                //Maneja el caso en el que nada falle
                return true;
            }
            else
            {
                // Maneja el caso en el que el producto o el ingrediente no existan
                return false;
            }
        }
    }
}
