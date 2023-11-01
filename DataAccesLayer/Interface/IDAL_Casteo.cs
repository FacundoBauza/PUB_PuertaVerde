using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Casteo
    {
        DTIngrediente GetDTIngrediente(Ingredientes x);
        DTCategoria GetDTCategoria(Categorias x);
        DTCliente_Preferencial CastDTCliente_Preferencial(ClientesPreferenciales x);
        DTProducto GetDTProducto(Productos c);
        DTMesa GetDTMesa(Mesas m);
        DTPedido CastDTPedido(Pedidos m);
        DTProducto_Observaciones CastDTPedidoProducto(Pedidos_Productos pp, Productos p);
        DTCaja GetDTCaja(Cajas c);
    }
}
