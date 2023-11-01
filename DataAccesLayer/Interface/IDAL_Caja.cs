using DataAccesLayer.Models;
using Domain.DT;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Caja
    {
        //Agregar
        bool Set_Caja(DTCaja dtc);
        //Listar
        List<Cajas> GetCaja();
        //Modificar
        public bool Modificar_Cajas(DTCaja dtc);
        //Baja
        bool Baja_Caja(int id);
    }
}
