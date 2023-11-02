using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Caja
    {
        //Agregar
        MensajeRetorno Set_Caja(DTCaja dtc);
        //Listar
        List<Cajas> GetCaja();
        List<Cajas> GetCajaactivas();
        //Modificar
        public bool Modificar_Cajas(DTCaja dtc);
        //Baja
        bool Baja_Caja(int id);
    }
}
