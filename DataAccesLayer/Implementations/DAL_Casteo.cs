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
                id_Mesa = m.Id_Mesa,
                enUso = m.EnUso,
                precioTotal = m.PrecioTotal
            };
            return aux;
        }

        public DTPedido CastDTPedido(Pedidos p)
        {
            DTPedido aux = new()
            {
                Id_Pedido = p.id_Pedido,
                ValorPedido = p.valorPedido,
                Pago = p.pago,
                EstadoProceso = p.estadoProceso,
                Username = p.username,
                Id_Cli_Preferencial = p.id_Cli_Preferencial,
                Id_Mesa = p.id_Mesa,
                Fecha_ingreso = p.fecha_ingreso,
                Numero_movil = p.numero_movil,
                Tipo = p.tipo
            };
            return aux;
        }

        public DTProducto_Observaciones CastDTPedidoProducto(Pedidos_Productos pp, Productos p)
        {
#pragma warning disable CS8601 // Posible asignación de referencia nula
            DTProducto_Observaciones aux = new()
            {
                Id_Producto = p.id_Producto,
                NombreProducto = p.nombre,
                Observaciones = pp.Observaciones,
                Tipo = p.tipo
            };
#pragma warning restore CS8601 // Posible asignación de referencia nula
            return aux;
        }

        public DTCaja GetDTCaja(Cajas c)
        {
            DTCaja dtc = new()
            {
                id = c.Id,
                estado = c.Estado,
                fecha = c.Fecha,
                TotalPrecios = c.TotalPrecios
            };
            return dtc;
        }
    }
}
