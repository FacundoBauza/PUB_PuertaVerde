using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_Mesa : IDAL_Mesa
    {
        private readonly DataContext _db;
        public DAL_Mesa(DataContext db)
        {
            _db = db;
        }

        public List<Mesas> getMesas()
        {
            return _db.Mesas.Select(x => x.GetMesa()).ToList();
        }

        public bool modificar_Mesas(DTMesa dtm)
        {
            // Utiliza SingleOrDefault() para buscar una Mesa.
            var MesaEncontrada = _db.Mesas.SingleOrDefault(i => i.id_Mesa == dtm.id_Mesa);
            if (MesaEncontrada != null)
            {
                // Modifica las propiedades del Producto.
                MesaEncontrada.enUso = dtm.enUso;
                MesaEncontrada.precioTotal = MesaEncontrada.precioTotal + dtm.precioTotal;
                // Guarda los cambios en la base de datos.
                _db.SaveChanges();

                return true;
            }
            else
                return false;
        }

        public bool set_Mesa(DTMesa dtm)
        {
            //Castea el DT en tipo Ingrediente
            Mesas aux = Mesas.SetMesa(dtm);
            try
            {
                //Agrega el Ingrediente
                _db.Mesas.Add(aux);

                // Guarda los cambios en la base de datos.
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
