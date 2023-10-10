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
        DTIngrediente getDTIngrediente(Ingredientes x);
        DTCategoria getDTCategoria(Categorias x);
        DTCliente_Preferencial castDTCliente_Preferencial(ClientesPreferenciales x);
        DTProducto getDTProducto(Productos c);
        DTMesa getDTMesa(Mesas m);
        DTPedido castDTPedido(Pedidos m);
    }
}
