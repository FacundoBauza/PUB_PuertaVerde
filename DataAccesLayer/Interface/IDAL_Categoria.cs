using DataAccesLayer.Models;
using Domain.DT;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
