using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
namespace DataAccesLayer.Implementations
{
    public class DAL_Estadisticas : IDAL_Estadisticas
    {
        private readonly DataContext _db;
        private readonly IDAL_Casteo _cas;

        public DAL_Estadisticas(DataContext db, IDAL_Casteo cas)
        {
            _db = db;
            _cas = cas;
        }

        public DTProductoEstadistica producto(DTProductoEstadistica value)
        {
            // Consulta con una operación "join"
            var query = from Pedidos_Productos in _db.Pedidos_Productos
                        join Pedidos in _db.Pedidos
                        on Pedidos_Productos.id_Pedido equals Pedidos.id_Pedido
                        where Pedidos_Productos.id_Producto == value.producto.id_Producto// Agregar la condición WHERE
                                && Pedidos.fecha_ingreso >= value.inicio
                                && Pedidos.fecha_ingreso <= value.fin
                        select new
                        {
                            id_Producto = Pedidos_Productos.id_Producto
                            // Agrega más campos según tus necesidades
                        };
            // Ejecuta la consulta y obtén los resultados
            var results = query.ToList();
            // Ahora puedes trabajar con los resultados
            value.cantidad = results.Count;
            return value;
        }

        public List<DTProductoEstadistica> productostipo(DTProductoEstadistica value)
        {
            List<DTProductoEstadistica> res = new();
            if (value != null)
            {
                List<Productos> producto = _db.Productos.Where(x => x.tipo == value.producto.tipo).Select(x => x.GetProducto()).ToList();
                foreach (var p in producto)
                {
                    // Crear una nueva instancia de DTProductoEstadistica en cada iteración
                    DTProductoEstadistica productoEstadistica = new();

                    // Consulta con una operación "join"
                    var query = from Pedidos_Productos in _db.Pedidos_Productos
                                join Pedidos in _db.Pedidos
                                on Pedidos_Productos.id_Pedido equals Pedidos.id_Pedido
                                where Pedidos_Productos.id_Producto == p.id_Producto
                                        && Pedidos.fecha_ingreso >= value.inicio
                                        && Pedidos.fecha_ingreso <= value.fin
                                select new
                                {
                                    id_Producto = Pedidos_Productos.id_Producto
                                    // Agrega más campos según tus necesidades
                                };
                    // Ejecuta la consulta y obtén los resultados
                    var results = query.ToList();
                    // Establecer las propiedades del nuevo objeto productoEstadistica
                    productoEstadistica.cantidad = results.Count;
                    productoEstadistica.producto = _cas.GetDTProducto(p);
                    res.Add(productoEstadistica);
                }
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
                DTProductoEstadistica productoEstadistica = new();

                // Consulta con una operación "join"
                var query = from Pedidos_Productos in _db.Pedidos_Productos
                            join Pedidos in _db.Pedidos
                            on Pedidos_Productos.id_Pedido equals Pedidos.id_Pedido
                            where Pedidos_Productos.id_Producto == p.id_Producto
                                    && Pedidos.fecha_ingreso >= value.inicio
                                    && Pedidos.fecha_ingreso <= value.fin
                            select new
                            {
                                id_Producto = Pedidos_Productos.id_Producto
                                // Agrega más campos según tus necesidades
                            };
                // Ejecuta la consulta y obtén los resultados
                var results = query.ToList();
                // Establecer las propiedades del nuevo objeto productoEstadistica
                productoEstadistica.cantidad = results.Count;
                productoEstadistica.producto = _cas.GetDTProducto(p);
                res.Add(productoEstadistica);
            }
            return res;
        }
    }
}