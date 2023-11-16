using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Mesa
    {
        List<Mesas> GetMesas();
        bool Modificar_Mesas(DTMesa dtm);
        bool Set_Mesa(DTMesa dtm);
        byte[] CerarMesa(int id);
        bool Baja_Mesa(int id);
        bool Modificar_Precio_Mesas(DTMesa modificar);

        bool AgregarPagoParcial(int id, float pagoEfectuado);
    }
}
