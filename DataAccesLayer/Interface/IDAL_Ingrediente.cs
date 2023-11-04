using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Ingrediente
    {
        //Agregar
        bool set_Ingrediente(DTIngrediente dti);

        //Listar
        List<Ingredientes> getIngrediente();

        //Modificar
        bool modificar_Ingrediente(DTIngrediente dti);
        MensajeRetorno Eliminar_Ingredente(int id);
    }
}

