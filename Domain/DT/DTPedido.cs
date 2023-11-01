using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTPedido
    {
        private Categoria tipo1;

        public int Id_Pedido { get; set; }
        public float ValorPedido { get; set; }
        public bool Pago { get; set; }
        public string Username { get; set; }
        public int Id_Cli_Preferencial { get; set; }
        public int Id_Mesa { get; set; }
        public bool EstadoProceso { get; set; }
        public DateTime Fecha_ingreso { get; set; }
        public string Numero_movil { get; set; }
        public Categoria Tipo { get => tipo1; set => tipo1 = value; }
        public List<DTProducto_Observaciones> List_IdProductos { get; set; }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public DTPedido()
        {
            Fecha_ingreso = DateTime.Today;
            Pago = false;
            EstadoProceso = false;
            List_IdProductos = new List<DTProducto_Observaciones>();
        }

        public DTPedido(int id_Pedido, float valorPedido, bool pago, string username, int id_Cli_Preferencial, int id_Mesa, string numero_movil,Categoria tipo)
        {
            this.Id_Pedido = id_Pedido;
            this.ValorPedido = valorPedido;
            this.Pago = pago;
            this.Username = username;
            this.Id_Cli_Preferencial = id_Cli_Preferencial;
            this.Id_Mesa = id_Mesa;
            this.Numero_movil = numero_movil;
            this.Tipo= tipo;
        }
    }
}
