using DataAccesLayer.Models;
using Domain.DT;
using Domain.Enums;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Pedido
    {
        //Agregar
        bool set_Cliente(DTPedido dtP);
        //Actualizar
        bool update_Pedido(DTPedido dtP);
        //Listar
        List<Pedidos> get_Pedidos();
        //Listar Productos Pedidos
        List<Pedidos_Productos> get_ProductosPedidos(int id_Pedido);
        //Listar Activos
        List<Pedidos> get_PedidosActivos();
        //Baja
        bool baja_Pedido(int id);
        //ProductoPedido
        Productos getProductoPedido(int id_Producto);
        bool finalizar_Pedido(int id);
        List<Pedidos> getPedidosPorTipo(Categoria tipo);
        List<Pedidos> getPedidosPorMesa(int id);
        Pedidos get_Pedido(int id);
    }
}
