using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Implementations
{
    public class B_Productos_Ingredientes : IBProductos_Ingredientes
    {
        private IDAL_ProductoIngrediente _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Productos_Ingredientes(IDAL_ProductoIngrediente dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }

        public MensajeRetorno Productos_Ingredientes(DTProductos_Ingredientes pi)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (_dal.ProductoIngrediente(pi.id_Producto, pi.id_Ingrediente))
            {
                men.mensaje = "El Ingrediente se guardo Correctamente";
                men.status = true;
                return men;
            }
            men.Objeto_Nulo();
            return men;
        }

        public List<DTIngrediente> listar_IngredientesProducto(int idProducto)
        {
            List<Ingredientes> Ingredientes = _dal.getIngredientesProducto(idProducto);
            List<DTIngrediente> dt_Ingredientes = new List<DTIngrediente>();
            foreach (Ingredientes i in Ingredientes)
            {
                dt_Ingredientes.Add(_cas.GetDTIngrediente(i));
            }

            return dt_Ingredientes;
        }

        public MensajeRetorno quitarProductos_Ingredientes(DTProductos_Ingredientes pi)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (_dal.bajaProductoIngrediente(pi.id_Producto, pi.id_Ingrediente))
            {
                men.mensaje = "El Ingrediente se quito Correctamente";
                men.status = true;
                return men;
            }
            men.Objeto_Nulo();
            return men;
        }
    }
}
