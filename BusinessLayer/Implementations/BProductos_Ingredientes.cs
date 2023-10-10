using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class BProductos_Ingredientes : IBProductos_Ingredientes
    {
        private IDAL_ProductoIngrediente _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public BProductos_Ingredientes(IDAL_ProductoIngrediente dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
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
                men.El_Ingrediente_se_guardo_Correctamente();
                return men;
            }
            men.Objeto_Nulo();
            return men;
        }
    }
}
