using System;
using System.IO;
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
    public class DAL_Estadisticas : IDAL_Estadisticas
    {
        private readonly DataContext _db;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public DAL_Estadisticas(DataContext db,IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _db = db;
            _cas = cas;
            _fu = fu;
        }

        public DTProductoEstadistica producto(DTProductoEstadistica value)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            // Consulta con una operación "join"
            var query = from Pedidos_Productos in _db.Pedidos_Productos
                        join Pedidos in _db.Pedidos
                        on Pedidos_Productos.id_Pedido equals Pedidos.id_Pedido
                        where Pedidos_Productos.id_Producto == value.Producto.id_Producto// Agregar la condición WHERE
                                && Pedidos.fecha_ingreso >= value.Inicio
                                && Pedidos.fecha_ingreso <= value.Fin
                        select new
                        {
                            id_Producto = Pedidos_Productos.id_Producto
                            // Agrega más campos según tus necesidades
                        };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            // Ejecuta la consulta y obtén los resultados
            var results = query.ToList();
            // Ahora puedes trabajar con los resultados
            value.Cantidad = results.Count;
            return value;
        }

        public List<DTProductoEstadistica> productostipo(DTProductoEstadistica value)
        {
            List<DTProductoEstadistica> res = new();
            List<Productos> producto = _db.Productos.Where(x => x.tipo == value.Producto.tipo).Select(x => x.GetProducto()).ToList();
            foreach (var p in producto)
            {
                // Crear una nueva instancia de DTProductoEstadistica en cada iteración
                DTProductoEstadistica productoEstadistica = new DTProductoEstadistica();

                // Consulta con una operación "join"
                var query = from Pedidos_Productos in _db.Pedidos_Productos
                            join Pedidos in _db.Pedidos
                            on Pedidos_Productos.id_Pedido equals Pedidos.id_Pedido
                            where Pedidos_Productos.id_Producto == p.id_Producto
                                    && Pedidos.fecha_ingreso >= value.Inicio
                                    && Pedidos.fecha_ingreso <= value.Fin
                            select new
                            {
                                id_Producto = Pedidos_Productos.id_Producto
                                // Agrega más campos según tus necesidades
                            };
                // Ejecuta la consulta y obtén los resultados
                var results = query.ToList();
                // Establecer las propiedades del nuevo objeto productoEstadistica
                productoEstadistica.Cantidad = results.Count;
                productoEstadistica.Producto = _cas.getDTProducto(p);
                res.Add(productoEstadistica);
            }
            return res;
        }


        public List<DTProductoEstadistica> todoslosproductos(DTProductoEstadistica value)
        {
            List<DTProductoEstadistica> res = new();
            List<Productos> producto = _db.Productos.Select(x => x.GetProducto()).ToList();
            foreach (var p in producto)
            {
                // Crear una nueva instancia de DTProductoEstadistica en cada iteración
                DTProductoEstadistica productoEstadistica = new DTProductoEstadistica();

                // Consulta con una operación "join"
                var query = from Pedidos_Productos in _db.Pedidos_Productos
                            join Pedidos in _db.Pedidos
                            on Pedidos_Productos.id_Pedido equals Pedidos.id_Pedido
                            where Pedidos_Productos.id_Producto == p.id_Producto
                                    && Pedidos.fecha_ingreso >= value.Inicio
                                    && Pedidos.fecha_ingreso <= value.Fin
                            select new
                            {
                                id_Producto = Pedidos_Productos.id_Producto
                                // Agrega más campos según tus necesidades
                            };
                // Ejecuta la consulta y obtén los resultados
                var results = query.ToList();
                // Establecer las propiedades del nuevo objeto productoEstadistica
                productoEstadistica.Cantidad = results.Count;
                productoEstadistica.Producto = _cas.getDTProducto(p);
                res.Add(productoEstadistica);
            }
            return res;
        }
    }
}