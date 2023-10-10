using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Ingrediente
    {
        public int id_Ingrediente { get; set; }
        public string nombre { get; set; }
        public int stock { get; set; }
        public int? id_Categoria { get; set; }

#pragma warning disable CS8618
        public Ingrediente()
        {
        }

        public Ingrediente(int id_Ingrediente, string nombre, int stock, int? id_Categoria)
        {
            this.id_Ingrediente = id_Ingrediente;
            this.nombre = nombre;
            this.stock = stock;
            this.id_Categoria = id_Categoria;
        }
    }
}
