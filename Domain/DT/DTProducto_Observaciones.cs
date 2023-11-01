using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTProducto_Observaciones
    {
        public int Id_Producto { get; set; }
        public string NombreProducto { get; set; }
        public string Observaciones { get; set; }
        public Categoria Tipo { get; set; }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public DTProducto_Observaciones()
        {
        }

        public DTProducto_Observaciones(int id_Producto, string observaciones, string nombreProducto)
        {
            this.Id_Producto = id_Producto;
            this.Observaciones = observaciones;
            this.NombreProducto = nombreProducto;
        }
    }
}
