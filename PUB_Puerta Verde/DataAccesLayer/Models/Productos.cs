using Domain.DT;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Models
{
    [Table(name: "Producto")]
    public class Productos
    {
#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_Producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public float precio { get; set; }
        public bool registro_Activo { get; set; }
        public ICollection<Productos_Ingredientes> ProductoIngredientes { get; set; }

        internal static Productos SetProducto(DTProducto p)
        {
            Productos aux = new Productos();
            aux.id_Producto = p.id_Producto;
            aux.nombre = p.nombre;
            aux.precio = p.precio;
            aux.descripcion = p.descripcion;
            return aux;
        }

        public Productos GetProducto()
        {
            Productos aux = new Productos();
            aux.id_Producto = id_Producto;
            aux.nombre = nombre;
            aux.precio = precio;
            aux.descripcion = descripcion;
            return aux;
        }
    }
}
