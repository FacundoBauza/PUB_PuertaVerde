using Domain.DT;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Estadisticas
    {
        DTProductoEstadistica producto(DTProductoEstadistica value);
        List<DTProductoEstadistica> productostipo(DTProductoEstadistica value);
        List<DTProductoEstadistica> todoslosproductos(DTProductoEstadistica value);
    }
}