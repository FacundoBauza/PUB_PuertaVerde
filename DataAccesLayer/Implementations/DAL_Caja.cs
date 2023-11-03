using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace DataAccesLayer.Implementations
{
    public class DAL_Caja : IDAL_Caja
    {
        private readonly DataContext _db;

        public DAL_Caja(DataContext db)
        {
            _db = db;
        }

        public MensajeRetorno Set_Caja(DTCaja dtc)
        {
            MensajeRetorno ret = new();
            //Castea el DT en tipo Caja
            Cajas aux = Cajas.SetCajas(dtc);
            try
            {
                if (!_db.Cajas.Where(x => x.estado).Any())
                {
                    //Agrega la Mesas
                    _db.Cajas.Add(aux);
                    // Guarda los cambios en la base de datos.
                    _db.SaveChanges();
                    ret.mensaje = "Caja agregada con exito";
                    ret.status = true;
                    return ret;
                }
                else {
                    ret.mensaje = "Ya hay na caja activa";
                    ret.status = false;
                    return ret;
                }
            }
            catch
            {
                //si ocurrio algun error retorna false
                ret.Exepcion_no_Controlada();
                return ret;
            }
        }
        public bool Modificar_Cajas(DTCaja dtc)
        {
            // Utiliza SingleOrDefault() para buscar una caja.
            var CajaEncontrada = _db.Cajas.SingleOrDefault(i => i.id == dtc.id);
            if (CajaEncontrada != null)
            {
                try
                {
                    // Modifica las propiedades de la mesa.
                    CajaEncontrada.estado = dtc.estado;
                    CajaEncontrada.totalPrecios = dtc.totalPrecios;
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

        public List<Cajas> GetCajaactivas()
        {
            //busca todas las cajas activas y las debuelve
            return _db.Cajas.Where(x => x.estado).Select(x => x.GetCajas()).ToList();
        }
    }
}
