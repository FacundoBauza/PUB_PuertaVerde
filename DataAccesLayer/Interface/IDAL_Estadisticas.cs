using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Estadisticas
    {
        DTProductoEstadistica producto(DTProductoEstadistica value);
        List<DTProductoEstadistica> productostipo(DTProductoEstadistica value);
        List<DTProductoEstadistica> todoslosproductos(DTProductoEstadistica value);
    }
}