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
        DTIngrediente IDAL_Casteo.getDTIngrediente(Ingredientes x)
        {
            DTIngrediente aux = new DTIngrediente();
            aux.id_Ingrediente = x.id_Ingrediente;
            aux.nombre = x.nombre;
            aux.stock = x.stock;
            aux.id_Categoria = x.id_Categoria;
            return aux;
        }

        DTProducto IDAL_Casteo.getDTProducto(Productos c)
        {
            DTProducto aux = new DTProducto();
            aux.id_Producto = c.id_Producto;
            aux.nombre = c.nombre;
            aux.precio = c.precio;
            aux.descripcion = c.descripcion;
            return aux;
        }

        DTCategoria IDAL_Casteo.getDTCategoria(Categorias x)
        {
            DTCategoria aux = new DTCategoria();
            aux.id_Categoria = x.id_Categoria;
            aux.nombre = x.nombre;
            return aux;
        }

        DTCliente_Preferencial IDAL_Casteo.castDTCliente_Preferencial(ClientesPreferenciales x)
        {
            DTCliente_Preferencial aux = new DTCliente_Preferencial();
            aux.id_Cli_Preferencial = x.id_Cli_Preferencial;
            aux.nombre = x.nombre;
            aux.apellido = x.apellido;
            aux.telefono = x.telefono;
            aux.saldo = x.saldo;
            aux.fichasCanje = x.fichasCanje;
            return aux;
        }

        public DTMesa getDTMesa(Mesas m)
        {
            DTMesa aux = new DTMesa();
            aux.id_Mesa = m.id_Mesa;
            aux.enUso = m.enUso;
            return aux;
        }

        public DTPedido castDTPedido(Pedidos p)
        {
            DTPedido aux = new DTPedido();
            aux.id_Pedido = p.id_Pedido;
            aux.valorPedido = p.valorPedido;
            aux.pago = p.pago;
            aux.estadoProceso = p.estadoProceso;
            aux.username = p.username;
            aux.id_Cli_Preferencial = p.id_Cli_Preferencial;
            aux.id_Mesa = p.id_Mesa;
            aux.fecha_ingreso = p.fecha_ingreso;
            aux.hora_ingreso = p.hora_ingreso;
            aux.numero_movil = p.numero_movil;
            return aux;
        }
    }
}
