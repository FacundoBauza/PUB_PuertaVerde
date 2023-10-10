using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_Categoria
    {
        //Agregar
        MensajeRetorno agregar_Categoria(DTCategoria dtc);

        //Listar
        List<DTCategoria> listar_Categoria();

        //Baja
        MensajeRetorno baja_Categoria(int id);
    }
}
