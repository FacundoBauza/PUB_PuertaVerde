using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
