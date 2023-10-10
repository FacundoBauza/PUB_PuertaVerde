using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
