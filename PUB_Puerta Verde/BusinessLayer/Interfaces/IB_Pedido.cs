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
        //ListarActivos
        List<DTPedido> listar_PedidosActivos();
        //Baja
        MensajeRetorno baja_Pedido(int id);
        //PDF
        byte[] pdf_Pedido(int id);

    }
}
