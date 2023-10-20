using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_Estadisticas
    {
        DTProductoEstadistica producto(DTProductoEstadistica value);
        List<DTProductoEstadistica> productostipo(DTProductoEstadistica value);
        List<DTProductoEstadistica> todoslosproductos(DTProductoEstadistica value);
    }
}