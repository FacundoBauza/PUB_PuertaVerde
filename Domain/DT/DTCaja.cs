namespace Domain.DT
{
    public class DTCaja
    {
        public int id { get; set; }
        public DateTime fecha { get; set; }
        public float totalPrecios { get; set; }
        public Boolean estado { get; set; }

        public DTCaja()
        {
        }

        public DTCaja(int id, DateTime fecha, float totalPrecios, bool estado)
        {
            this.id = id;
            this.fecha = fecha;
            this.totalPrecios = totalPrecios;
            this.estado = estado;
        }
    }
}
