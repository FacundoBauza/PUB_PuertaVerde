using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        //Listar Activos
        List<Pedidos> get_PedidosActivos();
        //Baja
        bool baja_Pedido(int id);
    }
}
