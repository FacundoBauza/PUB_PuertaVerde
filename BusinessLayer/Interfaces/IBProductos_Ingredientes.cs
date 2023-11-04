using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Interfaces
{
    public interface IBProductos_Ingredientes
    {
        MensajeRetorno Productos_Ingredientes(DTProductos_Ingredientes pi);
        List<DTIngrediente> listar_IngredientesProducto(int idProducto);
        MensajeRetorno quitarProductos_Ingredientes(DTProductos_Ingredientes pi);
    }
}
