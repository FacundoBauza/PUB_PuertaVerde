using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Interfaces
{
    public interface IB_Pedido
    {
        //Agregar
        MensajeRetorno agregar_Pedido(DTPedido dtP);
        //Actualizar
        MensajeRetorno actualizar_Pedido(DTPedido dtP);
        //Listar
        List<DTPedido> listar_Pedidos();
        List<DTPedido> listar_PedidosActivos();
        List<DTPedido> listar_PedidosPorTipo(Domain.Enums.Categoria tipo);
        List<DTPedido> listar_PedidosPorMesa(int id);
        //Baja
        MensajeRetorno baja_Pedido(int id);
        MensajeRetorno finalizar_Pedido(int id);
        DTPedido Pedido(int id);
    }
}
