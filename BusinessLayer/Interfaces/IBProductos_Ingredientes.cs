using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBProductos_Ingredientes
    {
        MensajeRetorno Productos_Ingredientes(DTProductos_Ingredientes pi);
    }
}
