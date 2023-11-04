using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Interfaces
{
    public interface IB_Producto
    {
        MensajeRetorno agregar_Producto(DTProducto value);
        MensajeRetorno baja_Producto(int id);
        List<DTProducto> listar_Productos();
        List<DTProducto> listar_ProductosPorTipo(Domain.Enums.Categoria tipo);
        MensajeRetorno Modificar_Producto(DTProducto modificar);
    }
}
