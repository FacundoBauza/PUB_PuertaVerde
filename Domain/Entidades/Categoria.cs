using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Categoria
    {
        public int id_Categoria { get; set; }
        public string nombre { get; set; }
        public List<Ingrediente> ingredientes { get; set; }

#pragma warning disable CS8618
        public Categoria()
        {
            ingredientes = new List<Ingrediente>();
        }

        public Categoria(int id_Categoria, string nombre)
        {
            this.id_Categoria = id_Categoria;
            this.nombre = nombre;
        }
    }
}
