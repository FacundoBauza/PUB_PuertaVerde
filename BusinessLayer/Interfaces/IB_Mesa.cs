using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_Mesa
    {
        MensajeRetorno agregar_Mesa(DTMesa value);
        MensajeRetorno baja_Mesa(int id);
        byte[] cerarMesa(DTMesa modificar);
        List<DTMesa> listar_Mesas();
        MensajeRetorno Modificar_Mesa(DTMesa modificar);
    }
}
