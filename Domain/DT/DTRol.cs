using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTRol
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DTRol() { }

        public DTRol(int id, string nombre)
        {
            this.id = id;   
            this.nombre = nombre;
        }

    }
}
