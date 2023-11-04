using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Interfaces
{
    public interface IB_Ingrediente
    {
        //Agregar
        MensajeRetorno Agregar_Ingrediente(DTIngrediente dti);

        //Listar
        List<DTIngrediente> Listar_Ingrediente();

        //Modificar
        MensajeRetorno Modificar_Ingrediente(DTIngrediente value);
    }
}
