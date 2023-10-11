using Domain.DT;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    [Table(name: "Pedido_Producto")]
    [PrimaryKey(nameof(id_Pedido), nameof(id_Producto))]
    public class Pedidos_Productos
    {
        public int id_Pedido { get; set; }
        public int id_Producto { get; set; }
        public string observaciones { get; set; }


        public static Pedidos_Productos SetPedido_Producto(int idPedido, int idProducto, string observaciones)
        {
            Pedidos_Productos aux = new Pedidos_Productos();

            aux.id_Pedido = idPedido;
            aux.id_Producto = idProducto;
            aux.observaciones = observaciones;

            return aux;
        }
        public Pedidos_Productos GetPedidos_Productos()
        {
            Pedidos_Productos aux = new Pedidos_Productos();
            aux.id_Producto = id_Producto;
            aux.id_Pedido = id_Pedido;
            aux.observaciones = observaciones;
            return aux;
        }
    }
}
