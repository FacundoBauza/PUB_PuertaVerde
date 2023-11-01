using Domain.Enums;

namespace Domain.DT
{
    public class DTPedido
    {
        private Categoria tipo1;

        public int id_Pedido { get; set; }
        public float valorPedido { get; set; }
        public bool pago { get; set; }
        public string username { get; set; }
        public int id_Cli_Preferencial { get; set; }
        public int id_Mesa { get; set; }
        public bool estadoProceso { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string numero_movil { get; set; }
        public Categoria tipo { get => tipo1; set => tipo1 = value; }
        public List<DTProducto_Observaciones> list_IdProductos { get; set; }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public DTPedido()
        {
            fecha_ingreso = DateTime.Today;
            pago = false;
            estadoProceso = false;
            list_IdProductos = new List<DTProducto_Observaciones>();
        }

        public DTPedido(int id_Pedido, float valorPedido, bool pago, string username, int id_Cli_Preferencial, int id_Mesa, string numero_movil, Categoria tipo)
        {
            this.id_Pedido = id_Pedido;
            this.valorPedido = valorPedido;
            this.pago = pago;
            this.username = username;
            this.id_Cli_Preferencial = id_Cli_Preferencial;
            this.id_Mesa = id_Mesa;
            this.numero_movil = numero_movil;
            this.tipo = tipo;
        }
    }
}
