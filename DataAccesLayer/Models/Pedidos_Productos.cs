using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccesLayer.Models
{
    [Table(name: "Pedido_Producto")]
    public class Pedidos_Productos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int idPedidoProducto { get; set; }
        public string? observaciones { get; set; }
        [ForeignKey("Pedidos")]
        public int id_Pedido { get; set; }
        [ForeignKey("Productos")]
        public int id_Producto { get; set; }


        public static Pedidos_Productos SetPedido_Producto(int idPedido, int idProducto, string observaciones)
        {
            Pedidos_Productos aux = new()
            {
                id_Pedido = idPedido,
                id_Producto = idProducto,
                observaciones = observaciones
            };

            return aux;
        }
        public Pedidos_Productos GetPedidos_Productos()
        {
            Pedidos_Productos aux = new()
            {
                idPedidoProducto = idPedidoProducto,
                id_Producto = id_Producto,
                id_Pedido = id_Pedido,
                observaciones = observaciones
            };
            return aux;
        }
    }
}
