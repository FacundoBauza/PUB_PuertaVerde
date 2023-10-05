using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class B_Pedido: IB_Pedido
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
            MensajeRetorno men = new MensajeRetorno();
            if (dtP != null)
            {
                if (!_fu.existePedido(dtP.id_Pedido))
                {
                    if (_fu.existeUsuario(dtP.username))
                    {
                        if (_fu.existeMesa(dtP.id_Mesa))
                        {
                            if (!_fu.mesaEnUso(dtP.id_Mesa))
                            {
                                if (_dal.set_Cliente(dtP) == true)
                                {
                                    _fu.agregarPrecioaMesa(dtP.valorPedido, dtP.id_Mesa);
                                    men.mensaje = "El Pedido se guardo Correctamente";
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
                                men.mensaje = "La mesa asignada ya esta en uso";
                                men.status = false;
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
                    men.mensaje = "Ya existe un pedido con los datos ingresados";
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
            foreach (Pedidos x in _dal.get_Pedidos())
            {
                pedidos.Add(_cas.castDTPedido(x));
            }

            return pedidos;
        }

        //Listar Activos
        List<DTPedido> IB_Pedido.listar_PedidosActivos()
        {
            List<DTPedido> pedidos = new List<DTPedido>();
            foreach (Pedidos x in _dal.get_PedidosActivos())
            {
                pedidos.Add(_cas.castDTPedido(x));
            }

            return pedidos;
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

        public byte[] pdf_Pedido(int id)
        {
            byte[] pdf_Pedido = _fu.pdfPedido(id);
            return pdf_Pedido;
        }
    }
}
