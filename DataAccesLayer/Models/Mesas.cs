using Domain.DT;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccesLayer.Models
{
    [Table(name: "Mesa")]
    public class Mesas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_Mesa { get; set; }
        public string? nombre { get; set; }
        public bool enUso { get; set; }
        public bool registro_Activo { get; set; }
        public float precioTotal { get; set; }

        internal static Mesas SetMesa(DTMesa p)
        {
            Mesas aux = new()
            {
                id_Mesa = p.id_Mesa,
                enUso = p.enUso,
                precioTotal = p.precioTotal,
                registro_Activo = true,
                nombre = p.nombre
            };
            return aux;
        }

        public Mesas GetMesa()
        {
            Mesas aux = new()
            {
                id_Mesa = id_Mesa,
                enUso = enUso,
                registro_Activo = registro_Activo,
                precioTotal = precioTotal,
                nombre = nombre
            };
            return aux;
        }
    }
}
