namespace Domain.DT
{
    public class DTProductos_Ingredientes
    {
        public int id_Producto { get; set; }
        public int id_Ingrediente { get; set; }

        public DTProductos_Ingredientes(int id_Producto, int id_Ingrediente)
        {
            this.id_Producto = id_Producto;
            this.id_Ingrediente = id_Ingrediente;
        }

        public DTProductos_Ingredientes()
        {
        }
    }
}
