using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_Producto: IDAL_Producto
    {
        private readonly DataContext _db;
        public DAL_Producto(DataContext db)
        {
            _db = db;
        }

        public List<Productos> getProducto()
        {
            //return _db.Productos.Select(x => x.GetProducto()).ToList(); 
            return _db.Productos
                .Where(producto => producto.ProductoIngredientes.All(ingrediente => ingrediente.Ingredientes.stock >0))
                .ToList();// ahora retorna  la lista de productos con stock en todos sus ingredientes.

        }

        public List<Productos> getProductoPorTipo(Domain.Enums.Categoria tipo)
        {
            return _db.Productos.Where(x => x.ProductoIngredientes.All(ingrediente => ingrediente.Ingredientes.stock > 0) & x.tipo == tipo).Select(x => x.GetProducto()).ToList();
        }

        public bool modificar_Producto(DTProducto dtp)
        {
            // Utiliza SingleOrDefault() para buscar un Producto por nombre.
            var ProductoEncontrado = _db.Productos.SingleOrDefault(i => i.id_Producto== dtp.id_Producto);
            if (ProductoEncontrado != null)
            {
                // Modifica las propiedades del Producto.
                ProductoEncontrado.nombre = dtp.nombre;
                ProductoEncontrado.precio = dtp.precio;
                ProductoEncontrado.descripcion = dtp.descripcion;
                ProductoEncontrado.tipo = dtp.tipo;
                // Guarda los cambios en la base de datos.
                _db.Update(ProductoEncontrado);
                _db.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public int set_Producto(DTProducto dtp)
        {
            //Castea el DT en tipo Ingrediente
            Productos aux = Productos.SetProducto(dtp);
            int idProducto = 0;
            try
            {
                //Agrega el Ingrediente
                _db.Productos.Add(aux);

                // Guarda los cambios en la base de datos.
                _db.SaveChanges();

                idProducto = aux.id_Producto;

                return idProducto;
            }
            catch
            {
                return 0;
            }
        }
    }
}
