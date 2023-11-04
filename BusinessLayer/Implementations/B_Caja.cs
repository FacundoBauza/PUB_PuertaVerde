using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

namespace BusinessLayer.Implementations
{
    public class B_Caja : IB_Caja
    {
        private readonly IDAL_Caja _dal;
        private readonly IDAL_Casteo _cas;

        public B_Caja(IDAL_Caja dal, IDAL_Casteo cas)
        {
            _dal = dal;
            _cas = cas;
        }

        public MensajeRetorno Baja_Cajas(int id)
        {
            throw new NotImplementedException();
        }

        public List<DTCaja> GetCajas()
        {
            List<DTCaja> dt_Caja = new();
            foreach (Cajas c in _dal.GetCaja())
            {
                dt_Caja.Add(_cas.GetDTCaja(c));
            }
            return dt_Caja;
        }

        public List<DTCaja> GetCajasactivas()
        {
            List<DTCaja> dt_Caja = new();
            foreach (Cajas c in _dal.GetCajaactivas())
            {
                dt_Caja.Add(_cas.GetDTCaja(c));
            }
            return dt_Caja;
        }

        public MensajeRetorno Modificar_Cajas(Domain.DT.DTCaja dtc)
        {
            MensajeRetorno men = new();
            if (dtc != null)
            {
                if (_dal.Modificar_Cajas(dtc) == true)
                {
                    men.mensaje = "La caja se guardo correctamente";
                    men.status = true;
                    return men;
                }
                else
                {
                    men.Exepcion_no_Controlada();
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }
        }

        public MensajeRetorno Set_Cajas(DTCaja dtc)
        {
            MensajeRetorno men = new();
            if (dtc != null)
            {
                men = _dal.Set_Caja(dtc);
                return men;
            }
            else
            {
                men.mensaje = "DTNULO";
                men.status = false;
                return men;
            }
        }
    }
}
