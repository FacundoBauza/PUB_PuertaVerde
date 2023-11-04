using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

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
            int aux = 0;
            if (dtp != null)
            {
                if (!_fu.existeProducto(dtp.nombre))
                {
                    aux = _dal.set_Producto(dtp);
                    if (aux != 0)
                    {
                        men.mensaje = "El producto se guardo correctamente/"+aux;
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
                    men.mensaje = "Ya existe el Producto";
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

        public List<DTProducto> listar_Productos()
        {
            List<Productos> Productos = _dal.getProducto();
            List<DTProducto> dt_Productos = new List<DTProducto>();
            foreach (Productos c in Productos)
            {
                dt_Productos.Add(_cas.GetDTProducto(c));
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
                    men.mensaje = "El producto se guardo correctamente";
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
                men.Objeto_Nulo();
                return men;
            }
        }

        public MensajeRetorno baja_Producto(int id)
        {
            throw new NotImplementedException();
        }

        public List<DTProducto> listar_ProductosPorTipo(Domain.Enums.Categoria tipo)
        {
            List<Productos> Productos = _dal.getProductoPorTipo(tipo);
            List<DTProducto> dt_Productos = new List<DTProducto>();
            foreach (Productos c in Productos)
            {
                dt_Productos.Add(_cas.GetDTProducto(c));
            }
            return dt_Productos;
        }
    }
}
