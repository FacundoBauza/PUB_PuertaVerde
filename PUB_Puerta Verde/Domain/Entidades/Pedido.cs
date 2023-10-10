using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Pedido
    {
        public int id_Pedido { get; set; }
        public float valorPedido { get; set; }
        public bool pago { get; set; }
        public string username { get; set; }
        public int id_Cli_Preferencial { get; set; }
        public int id_Mesa { get; set; }
        public bool estadoProceso { get; set; }
        public DateTime hora_ingreso { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string numero_movil { get; set; }

#pragma warning disable CS8618
        public Pedido()
        {
            hora_ingreso = DateTime.Today;
            fecha_ingreso = DateTime.Today;
            pago = false;
            estadoProceso = false;
        }

        public Pedido(int id_Pedido, float valorPedido, bool pago, string username, int id_Cli_Preferencial, int id_Mesa, string numero_movil)
        {
            this.id_Pedido = id_Pedido;
            this.valorPedido = valorPedido;
            this.pago = pago;
            this.username = username;
            this.id_Cli_Preferencial = id_Cli_Preferencial;
            this.id_Mesa = id_Mesa;
            this.numero_movil = numero_movil;
        }
    }
}
