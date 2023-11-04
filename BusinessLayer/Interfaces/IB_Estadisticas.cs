using Domain.DT;

namespace BusinessLayer.Interfaces
{
    public interface IB_Estadisticas
    {
        DTProductoEstadistica producto(DTProductoEstadistica value);
        List<DTProductoEstadistica> productostipo(DTProductoEstadistica value);
        List<DTProductoEstadistica> todoslosproductos(DTProductoEstadistica value);
    }
}