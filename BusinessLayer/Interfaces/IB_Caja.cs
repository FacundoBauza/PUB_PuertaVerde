using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Interfaces
{
    public interface IB_Caja
    {
        //Agregar
        MensajeRetorno Set_Cajas(Domain.DT.DTCaja dtc);
        //Listar
        List<DTCaja> GetCajas();
        List<DTCaja> GetCajasactivas();
        //Modificar
        public MensajeRetorno Modificar_Cajas(Domain.DT.DTCaja dtc);
        //Baja
        MensajeRetorno Baja_Cajas(int id);
    }
}
