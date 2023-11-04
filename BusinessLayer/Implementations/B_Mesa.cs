using BusinessLayer.Interfaces;
using DataAccesLayer.Implementations;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace BusinessLayer.Implementations
{
    public class B_Mesa : IB_Mesa
    {

        private IDAL_Mesa _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Mesa(IDAL_Mesa dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }
        public MensajeRetorno Agregar_Mesa(DTMesa dtm)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtm != null)
            {
                if (!_fu.existeMesa(dtm.id_Mesa))
                {
                    if (_dal.Set_Mesa(dtm) == true)
                    {
                        men.mensaje = "La mesa se guardo correctamente";
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
                    men.mensaje = "Ya existe la mesa";
                    men.status = false;
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }
        }

        public List<DTMesa> Listar_Mesas()
        {
            List<Mesas> Mesas = _dal.GetMesas();
            List<DTMesa> dt_Mesas = new List<DTMesa>();
            foreach (Mesas m in Mesas)
            {
                dt_Mesas.Add(_cas.GetDTMesa(m));
            }

            return dt_Mesas;
        }

        public MensajeRetorno Modificar_Mesa(DTMesa dtm)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtm != null)
            {
                if (_dal.Modificar_Mesas(dtm) == true)
                {
                    men.mensaje = "La mesa se guardo correctamente";
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

        public MensajeRetorno Baja_Mesa(int id)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (_dal.Baja_Mesa(id) == true)
            {
                men.mensaje = "La mesa se dio de baja correctamente";
                men.status = true;
                return men;
            }
            else
            {
                men.Exepcion_no_Controlada();
                return men;
            }
        }


        public byte[] CerarMesa(DTMesa modificar)
        {
            return _dal.CerarMesa(modificar.id_Mesa);
        }
    }
}
