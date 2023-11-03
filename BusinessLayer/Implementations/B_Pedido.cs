using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Implementations
{
    public class B_Pedido : IB_Pedido
    {
        private IDAL_Pedido _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Pedido(IDAL_Pedido dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }

        //Agregar
        MensajeRetorno IB_Pedido.agregar_Pedido(DTPedido dtP)
        {
            MensajeRetorno men = new();
            if (dtP != null)
            {
                if (dtP.list_IdProductos != null)
                {
                    if (!_fu.existePedido(dtP.id_Pedido))
                    {
                        if (_fu.existeUsuario(dtP.username))
                        {
                            if (_dal.set_Cliente(dtP))
                            {
                                if (_fu.existeClienteId(dtP.id_Cli_Preferencial))
                                {
                                    _fu.restarSaldoCliente(dtP.valorPedido, dtP.id_Cli_Preferencial);
                                    men.mensaje = "El Pedido se guardo Correctamente en el cliente";
                                    men.status = true;
                                    return men;
                                }
                                else if (_fu.existeMesa(dtP.id_Mesa))
                                {
                                    _fu.agregarPrecioaMesa(dtP.valorPedido, dtP.id_Mesa);
                                    men.mensaje = "El Pedido se guardo Correctamente en la mesa";
                                    men.status = true;
                                    return men;
                                }
                                else
                                {
                                    men.mensaje = "El Pedido se guardo Correctamente sin mesa ni cliente";
                                    men.status = true;
                                    return men;
                                }
                            }
                            else
                            {
                                men.Exepcion_no_Controlada();
                                return men;
                            }
                        }
                        else
                        {
                            men.mensaje = "El usuario no existe";
                            men.status = false;
                            return men;
                        }
                    }
                    else
                    {
                        men.mensaje = "Ya existe un pedido con los datos ingresados";
                        men.status = false;
                        return men;
                    }
                }
                else
                {
                    men.mensaje = "El Pedido no cuenta con profuctos";
                    men.status = false;
                    return men;
                }
            }
            men.Objeto_Nulo();
            return men;
        }

        //Actualizar
        MensajeRetorno IB_Pedido.actualizar_Pedido(DTPedido dtP)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtP != null)
            {
                if (_fu.existePedido(dtP.id_Pedido))
                {
                    if (_fu.existeUsuario(dtP.username))
                    {
                        if (_fu.existeMesa(dtP.id_Mesa))
                        {
                            if (_dal.update_Pedido(dtP) == true)
                            {
                                _fu.agregarPrecioaMesa(dtP.valorPedido, dtP.id_Mesa);
                                men.mensaje = "El Pedido se actualizo Correctamente";
                                men.status = true;
                                return men;
                            }
                            else
                            {
                                men.Exepcion_no_Controlada();
                                return men;
                            }
                        }
                        else
                        {
                            men.mensaje = "La mesa asignada no existe en el sistema";
                            men.status = false;
                            return men;
                        }
                    }
                    else
                    {
                        men.mensaje = "El usuario no existe";
                        men.status = false;
                        return men;
                    }
                }
                else
                {
                    men.mensaje = "No existe un pedido con los datos ingresados";
                    men.status = false;
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }
        }

        //Listar
        List<DTPedido> IB_Pedido.listar_Pedidos()
        {
            List<DTPedido> pedidos = new List<DTPedido>();
            DTPedido? pedido = null;
            foreach (Pedidos x in _dal.get_Pedidos())
            {
                pedido = _cas.CastDTPedido(x);
                foreach (Pedidos_Productos x1 in _dal.get_ProductosPedidos(x.id_Pedido))
                {
                    pedido.list_IdProductos.Add(_cas.CastDTPedidoProducto(x1, _dal.getProductoPedido(x1.id_Producto)));
                }
                pedidos.Add(pedido);
            }

            return pedidos;
        }

        //Listar Activos
        List<DTPedido> IB_Pedido.listar_PedidosActivos()
        {
            List<DTPedido> pedidos = new List<DTPedido>();
            DTPedido? pedido = null;
            foreach (Pedidos x in _dal.get_PedidosActivos())
            {
                pedido = _cas.CastDTPedido(x);
                foreach (Pedidos_Productos x1 in _dal.get_ProductosPedidos(x.id_Pedido))
                {
                    pedido.list_IdProductos.Add(_cas.CastDTPedidoProducto(x1, _dal.getProductoPedido(x1.id_Producto)));
                }
                pedidos.Add(pedido);
            }
            return pedidos;
        }
        public List<DTPedido> listar_PedidosPorTipo(Domain.Enums.Categoria tipo)
        {
            List<DTPedido> dt_Productos = new List<DTPedido>();
            foreach (Pedidos x in _dal.getPedidosPorTipo(tipo))
            {
                DTPedido pedido = _cas.CastDTPedido(x);
                foreach (Pedidos_Productos x1 in _dal.get_ProductosPedidos(x.id_Pedido))
                {
                    pedido.list_IdProductos.Add(_cas.CastDTPedidoProducto(x1, _dal.getProductoPedido(x1.id_Producto)));
                }
                dt_Productos.Add(pedido);
            }
            return dt_Productos;
        }
        public List<DTPedido> listar_PedidosPorMesa(int id)
        {
            List<DTPedido> dt_Productos = new List<DTPedido>();
            foreach (Pedidos x in _dal.getPedidosPorMesa(id))
            {
                DTPedido pedido = _cas.CastDTPedido(x);
                foreach (Pedidos_Productos x1 in _dal.get_ProductosPedidos(x.id_Pedido))
                {
                    pedido.list_IdProductos.Add(_cas.CastDTPedidoProducto(x1, _dal.getProductoPedido(x1.id_Producto)));
                }
                dt_Productos.Add(pedido);
            }
            return dt_Productos;
        }

        MensajeRetorno IB_Pedido.baja_Pedido(int id)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (_dal.baja_Pedido(id) == true)
            {
                men.mensaje = "El Pedido se dio de baja correctamente";
                men.status = true;
                return men;
            }
            else
            {
                men.Exepcion_no_Controlada();
                return men;
            }
        }
        public MensajeRetorno finalizar_Pedido(int id)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (_dal.finalizar_Pedido(id) == true)
            {
                men.mensaje = "El Pedido se finalizo correctamente";
                men.status = true;
                return men;
            }
            else
            {
                men.Exepcion_no_Controlada();
                return men;
            }
        }

        public DTPedido Pedido(int id)
        {
            DTPedido? pedido = _cas.CastDTPedido(_dal.get_Pedido(id));
            foreach (Pedidos_Productos x1 in _dal.get_ProductosPedidos(id))
            {
                pedido.list_IdProductos.Add(_cas.CastDTPedidoProducto(x1, _dal.getProductoPedido(x1.id_Producto)));
            }
            return pedido;
        }
    }
}
