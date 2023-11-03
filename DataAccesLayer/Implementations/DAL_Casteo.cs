using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;

namespace DataAccesLayer.Implementations
{
    public class DAL_Casteo : IDAL_Casteo
    {
        DTIngrediente IDAL_Casteo.GetDTIngrediente(Ingredientes x)
        {
            DTIngrediente aux = new()
            {
                id_Ingrediente = x.id_Ingrediente,
                nombre = x.nombre,
                stock = x.stock,
                id_Categoria = x.id_Categoria
            };
            return aux;
        }

        DTProducto IDAL_Casteo.GetDTProducto(Productos c)
        {
            DTProducto aux = new()
            {
                id_Producto = c.id_Producto,
                nombre = c.nombre,
                precio = c.precio,
                descripcion = c.descripcion,
                tipo = c.tipo
            };
            return aux;
        }

        DTCategoria IDAL_Casteo.GetDTCategoria(Categorias x)
        {
            DTCategoria aux = new()
            {
                id_Categoria = x.id_Categoria,
                nombre = x.nombre
            };
            return aux;
        }

        DTCliente_Preferencial IDAL_Casteo.CastDTCliente_Preferencial(ClientesPreferenciales x)
        {
            DTCliente_Preferencial aux = new()
            {
                id_Cli_Preferencial = x.id_Cli_Preferencial,
                nombre = x.nombre,
                apellido = x.apellido,
                telefono = x.telefono,
                saldo = x.saldo,
                fichasCanje = x.fichasCanje
            };
            return aux;
        }

        public DTMesa GetDTMesa(Mesas m)
        {
            DTMesa aux = new()
            {
                id_Mesa = m.id_Mesa,
                enUso = m.enUso,
                nombre = m.nombre,
                precioTotal = m.precioTotal
            };
            return aux;
        }

        public DTPedido CastDTPedido(Pedidos p)
        {
            DTPedido aux = new()
            {
                id_Pedido = p.id_Pedido,
                valorPedido = p.valorPedido,
                pago = p.pago,
                estadoProceso = p.estadoProceso,
                username = p.username,
                id_Cli_Preferencial = p.id_Cli_Preferencial,
                id_Mesa = p.id_Mesa,
                fecha_ingreso = p.fecha_ingreso,
                numero_movil = p.numero_movil,
                tipo = p.tipo
            };
            return aux;
        }

        public DTProducto_Observaciones CastDTPedidoProducto(Pedidos_Productos pp, Productos p)
        {
#pragma warning disable CS8601 // Posible asignación de referencia nula
            DTProducto_Observaciones aux = new()
            {
                id_Producto = p.id_Producto,
                nombreProducto = p.nombre,
                observaciones = pp.observaciones,
                tipo = p.tipo
            };
#pragma warning restore CS8601 // Posible asignación de referencia nula
            return aux;
        }

        public DTCaja GetDTCaja(Cajas c)
        {
            DTCaja dtc = new()
            {
                id = c.id,
                estado = c.estado,
                fecha = c.fecha,
                totalPrecios = c.totalPrecios
            };
            return dtc;
        }
    }
}
