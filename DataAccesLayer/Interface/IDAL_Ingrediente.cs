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

