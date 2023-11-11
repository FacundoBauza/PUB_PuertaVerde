namespace Domain.DT
{
    public class DTProductoEstadistica
    {
        public int cantidad { get; set; }
        public DateTime inicio { get; set; }
        public DateTime fin { get; set; }
        public DTProducto producto { get; set; }

        public DTProductoEstadistica(int cantidad, DateTime inicio, DateTime fin, DTProducto producto)
        {
            this.cantidad = cantidad;
            this.inicio = inicio;
            this.fin = fin;
            this.producto = producto;
        }

        public DTProductoEstadistica()
        {
        }

    }
}
