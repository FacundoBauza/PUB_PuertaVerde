using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using Domain.DT;

namespace BusinessLayer.Implementations
{
    public class B_Estadisticas : IB_Estadisticas
    {
        private IDAL_Estadisticas _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Estadisticas(IDAL_Estadisticas dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }

        public DTProductoEstadistica producto(DTProductoEstadistica value)
        {
            return _dal.producto(value);
        }

        public List<DTProductoEstadistica> productostipo(DTProductoEstadistica value)
        {
            return _dal.productostipo(value);
        }

        public List<DTProductoEstadistica> todoslosproductos(DTProductoEstadistica value)
        {
            return _dal.todoslosproductos(value);
        }
    }
}