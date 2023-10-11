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
                try
                {
                    // Modifica las propiedades de la mesa.
                    MesaEncontrada.enUso = dtm.enUso;
                    MesaEncontrada.precioTotal = dtm.precioTotal;
                    // Guarda los cambios en la base de datos.
                    _db.Update(MesaEncontrada);
                    _db.SaveChanges();
                    //retota que todo se hizo corectamente
                    return true;
                }
                catch { }
            }
            //no se pudo encontrar la mesa y retorna false
            return false;
        }

        public bool set_Mesa(DTMesa dtm)
        {
            //Castea el DT en tipo Mesas
            Mesas aux = Mesas.SetMesa(dtm);
            try
            {
                //Agrega la Mesas
                _db.Mesas.Add(aux);

                // Guarda los cambios en la base de datos.
                _db.SaveChanges();
            }
            catch
            {
                //si ocurrio algun error retorna false
                return false;
            }
            //todo bien y retorna true
            return true;
        }
    }
}
