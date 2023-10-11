using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTPedido
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
        public List<DTProducto_Observaciones> list_IdProductos { get; set; }

        public DTPedido()
        {
            hora_ingreso = DateTime.Today;
            fecha_ingreso = DateTime.Today;
            pago = false;
            estadoProceso = false;
            list_IdProductos = new List<DTProducto_Observaciones>();
        }

        public DTPedido(int id_Pedido, float valorPedido, bool pago, string username, int id_Cli_Preferencial, int id_Mesa, string numero_movil)
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
