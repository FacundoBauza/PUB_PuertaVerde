using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_ClientePreferencial
    {
        //Agregar
        MensajeRetorno agregar_ClientePreferencial(DTCliente_Preferencial dtCP);
        //Actualizar
        MensajeRetorno actualizar_ClientePreferencial(DTCliente_Preferencial dtCP);
        //Listar
        List<DTCliente_Preferencial> listar_ClientePreferencial();
        //Baja
        MensajeRetorno baja_ClientePreferencial(int id);
    }
}
