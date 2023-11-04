using DataAccesLayer.Models;
using Domain.DT;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Categoria
    {
        //Agregar
        bool set_Categoria(DTCategoria dtc);

        //Listar
        List<Categorias> getCategorias();

        //Baja
        bool baja_Categoria(int id);
    }
}
