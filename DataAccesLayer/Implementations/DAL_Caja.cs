using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_Caja : IDAL_Caja
    {
        private readonly DataContext _db;

        public DAL_Caja(DataContext db)
        {
            _db = db;
        }

        public bool Set_Caja(DTCaja dtc)
        {
            //Castea el DT en tipo Caja
            Cajas aux = Cajas.SetCajas(dtc);
            try
            {
                //Agrega la Mesas
                _db.Cajas.Add(aux);
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
        public bool Modificar_Cajas(DTCaja dtc)
        {
            // Utiliza SingleOrDefault() para buscar una caja.
            var CajaEncontrada = _db.Cajas.SingleOrDefault(i => i.Id== dtc.id);
            if (CajaEncontrada != null)
            {
                try
                {
                    // Modifica las propiedades de la mesa.
                    CajaEncontrada.Estado = dtc.estado;
                    CajaEncontrada.TotalPrecios = dtc.TotalPrecios;
                    // Guarda los cambios en la base de datos.
                    _db.Cajas.Update(CajaEncontrada);
                    _db.SaveChanges();

                    //retota que todo se hizo corectamente
                    return true;
                }
                catch { }
            }
            //no se pudo encontrar la mesa y retorna false
            return false;
        }
        public List<Cajas> GetCaja()
        {
            //busca todas las cajas y las debuelve
            return _db.Cajas.Select(x => x.GetCajas()).ToList();
        }
        public bool Baja_Caja(int id)
        {
            throw new NotImplementedException();
        }

    }
}
