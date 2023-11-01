using Domain.DT;
using Domain.Entidades;

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
        //cerrar la cuenta dejando pagos los pedidos y el saldo en 0 (si le queda saldo a favor o algo se modifica aparte)
        byte[] cerarCuenta(DTCliente_Preferencial modificar);
    }
}
