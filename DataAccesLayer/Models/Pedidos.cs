using Domain.DT;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccesLayer.Models
{
    [Table(name: "Pedido")]
    public class Pedidos
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_Pedido { get; set; }
        public float valorPedido { get; set; }
        public bool estadoProceso { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string numero_movil { get; set; }
        public bool pago { get; set; }
        public Domain.Enums.Categoria tipo { get; set; }

        [ForeignKey("User")]
        public string username { get; set; }

        [ForeignKey("Cliente_Preferencial")]
        public int id_Cli_Preferencial { get; set; }

        [ForeignKey("Mesa")]
        public int id_Mesa { get; set; }


        public static Pedidos SetPedido(DTPedido x)
        {
            Pedidos aux = new Pedidos
            {
                id_Pedido = x.id_Pedido,
                valorPedido = x.valorPedido,
                estadoProceso = true,
                pago = false,
                fecha_ingreso = x.fecha_ingreso,
                numero_movil = x.numero_movil,
                username = x.username,
                id_Cli_Preferencial = x.id_Cli_Preferencial,
                id_Mesa = x.id_Mesa,
                tipo = x.tipo,
            };

            return aux;
        }

        public Pedidos GetPedido()
        {
            Pedidos aux = new Pedidos
            {
                id_Pedido = id_Pedido,
                valorPedido = valorPedido,
                estadoProceso = estadoProceso,
                pago = pago,
                fecha_ingreso = fecha_ingreso,
                numero_movil = numero_movil,
                username = username,
                id_Cli_Preferencial = id_Cli_Preferencial,
                id_Mesa = id_Mesa,
                tipo = tipo
            };
            return aux;
        }
    }
}
