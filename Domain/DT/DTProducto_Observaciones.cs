using Domain.Enums;

namespace Domain.DT
{
    public class DTProducto_Observaciones
    {
        public int id_Producto { get; set; }
        public string nombreProducto { get; set; }
        public string observaciones { get; set; }
        public Categoria tipo { get; set; }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public DTProducto_Observaciones()
        {
        }

        public DTProducto_Observaciones(int id_Producto, string observaciones, string nombreProducto)
        {
            this.id_Producto = id_Producto;
            this.observaciones = observaciones;
            this.nombreProducto = nombreProducto;
        }
    }
}
