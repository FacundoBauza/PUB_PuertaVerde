using BusinessLayer.Interfaces;
using DataAccesLayer.Implementations;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class B_Producto : IB_Producto
    {

        private IDAL_Producto _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Producto(IDAL_Producto dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }
        public MensajeRetorno agregar_Producto(DTProducto dtp)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtp != null)
            {
                if (!_fu.existeProducto(dtp.nombre))
                {
                    if (_dal.set_Producto(dtp) == true)
                    {
                        men.El_Ingrediente_se_guardo_Correctamente();
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
                    men.Ya_existe_un_Ingrediente_con_el_Nombre_ingresado();
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }
        }

        public List<DTProducto> listar_Productos()
        {
            List<Productos> Productos = _dal.getProducto();
            List<DTProducto> dt_Productos = new List<DTProducto>();
            foreach (Productos c in Productos)
            {
                dt_Productos.Add(_cas.getDTProducto(c));
            }

            return dt_Productos;
        }

        public MensajeRetorno Modificar_Producto(DTProducto dtp)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtp != null)
            {
                if (_dal.modificar_Producto(dtp) == true)
                {
                    men.El_Ingrediente_se_guardo_Correctamente();
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
                men.Objeto_Nulo();
                return men;
            }
        }

        public MensajeRetorno baja_Producto(int id)
        {
            throw new NotImplementedException();
        }
    }
}
