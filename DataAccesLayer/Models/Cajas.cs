using Domain.DT;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccesLayer.Models
{
    [Table(name: "Cajas")]
    public class Cajas
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id { get; set; }

        public DateTime fecha { get; set; }

        public float totalPrecios { get; set; }

        public Boolean estado { get; set; }

        public Cajas GetCajas()
        {
            Cajas aux = new()
            {
                id = id,
                fecha = fecha,
                totalPrecios = totalPrecios,
                estado = estado
            };
            return aux;
        }
        internal static Cajas SetCajas(DTCaja p)
        {
            Cajas aux = new()
            {
                id = p.id,
                fecha = p.fecha,
                totalPrecios = p.totalPrecios,
                estado = p.estado
            };
            return aux;
        }
    }
}
