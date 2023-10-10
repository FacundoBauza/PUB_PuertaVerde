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
    public class DAL_Pedido : IDAL_Pedido
    {
        private readonly DataContext _db;
        public DAL_Pedido(DataContext db)
        {
            _db = db;
        }

        //Agregar
        public bool set_Cliente(DTPedido dtP)
        {
            Pedidos aux = Pedidos.SetPedido(dtP);
            Pedidos_Productos? aux2 = null;
            try
            {
                _db.Pedidos.Add(aux);
                _db.SaveChanges();

                int idPedido = aux.id_Pedido;

                foreach (DTProducto_Observaciones dpo in dtP.list_IdProductos)
                {
                    aux2 = Pedidos_Productos.SetPedido_Producto(idPedido, dpo.id_Producto, dpo.observaciones);
                    _db.Pedidos_Productos.Add(aux2);
                    _db.SaveChanges();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Actualizar
        bool IDAL_Pedido.update_Pedido(DTPedido dtP)
        {
            Pedidos? aux = null;
            aux = _db.Pedidos.FirstOrDefault(pe => pe.id_Pedido == dtP.id_Pedido);
            if (aux != null)
            {
                aux.valorPedido = dtP.valorPedido;
                aux.pago = dtP.pago;
                aux.username = dtP.username;
                aux.id_Cli_Preferencial = dtP.id_Cli_Preferencial;
                aux.estadoProceso = dtP.estadoProceso;
                aux.fecha_ingreso = dtP.fecha_ingreso;
                aux.hora_ingreso = dtP.hora_ingreso;
                aux.numero_movil = dtP.numero_movil;
                try
                {
                    _db.Update(aux);
                    _db.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        //Listar Activos 
        List<Pedidos> IDAL_Pedido.get_PedidosActivos()
        {
            return _db.Pedidos.Where(x => x.estadoProceso).Select(x => x.GetPedido()).ToList();
        }

        //Listar
        List<Pedidos> IDAL_Pedido.get_Pedidos()
        {
            return _db.Pedidos.Select(x => x.GetPedido()).ToList();
        }

        //Baja 
        bool IDAL_Pedido.baja_Pedido(int id)
        {
            Pedidos? aux = null;
            aux = _db.Pedidos.FirstOrDefault(ped => ped.id_Pedido == id);
            if(aux != null) {
            aux.estadoProceso = false;

            try
            {
                _db.Update(aux);
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
            }
            return false;
        }
    }
}
