using DataAccesLayer.Models;
using Domain.DT;

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
    }
}

