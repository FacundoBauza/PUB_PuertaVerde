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
        MensajeRetorno Agregar_Mesa(DTMesa value);
        MensajeRetorno Baja_Mesa(int id);
        byte[] CerarMesa(DTMesa modificar);
        List<DTMesa> Listar_Mesas();
        MensajeRetorno Modificar_Mesa(DTMesa modificar);
    }
}
