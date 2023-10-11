using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_Producto
    {
        MensajeRetorno agregar_Producto(DTProducto value);
        MensajeRetorno baja_Producto(int id);
        List<DTProducto> listar_Productos();
        MensajeRetorno Modificar_Producto(DTProducto modificar);
    }
}
