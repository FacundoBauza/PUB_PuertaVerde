namespace Domain.DT
{
    public class DTPDF
    {
        public string nombre { get; set; }
        public int cantidad { get; set; }
        public float precio { get; set; }

        public DTPDF(string nombre, int cantidad, float precio)
        {
            this.nombre = nombre;
            this.cantidad = cantidad;
            this.precio = precio;
        }
    }
}
