using Domain.DT;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    [Table(name: "Pedido_Producto")]
    public class Pedidos_Productos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdPedidoProducto { get; set; }
        public string? Observaciones { get; set; }
        [ForeignKey("Pedidos")]
        public int Id_Pedido { get; set; }
        [ForeignKey("Productos")]
        public int Id_Producto { get; set; }


        public static Pedidos_Productos SetPedido_Producto(int idPedido, int idProducto, string observaciones)
        {
            Pedidos_Productos aux = new()
            {
                Id_Pedido = idPedido,
                Id_Producto = idProducto,
                Observaciones = observaciones
            };

            return aux;
        }
        public Pedidos_Productos GetPedidos_Productos()
        {
            Pedidos_Productos aux = new()
            {
                IdPedidoProducto = IdPedidoProducto,
                Id_Producto = Id_Producto,
                Id_Pedido = Id_Pedido,
                Observaciones = Observaciones
            };
            return aux;
        }
    }
}
