using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
namespace DataAccesLayer.Implementations
{
    public class DAL_Estadisticas : IDAL_Estadisticas
    {
        private readonly DataContext _db;
        public DAL_Estadisticas(DataContext db)
        {
            _db = db;
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
            foreach (var result in results)
            {
                value.Cantidad++;
            }
            return value;
        }
    }
}