namespace Domain.DT
{
    public class DTMesa
    {
        public int id_Mesa { get; set; }
        public bool enUso { get; set; }
        public float precioTotal { get; set; }

        public DTMesa()
        {
        }

        public DTMesa(int id_Mesa, bool enUso, float precioTotal)
        {
            this.id_Mesa = id_Mesa;
            this.enUso = enUso;
            this.precioTotal = precioTotal;
        }
    }
}
