using BusinessLayer.Interfaces;
using DataAccesLayer.Implementations;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
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
        public MensajeRetorno agregar_Mesa(DTMesa dtm)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtm != null)
            {
                if (!_fu.existeMesa(dtm.id_Mesa))
                {
                    if (_dal.set_Mesa(dtm) == true)
                    {
                        men.El_Ingrediente_se_guardo_Correctamente();
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
                    men.Ya_existe_un_Ingrediente_con_el_Nombre_ingresado();
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }
        }

        public List<DTMesa> listar_Mesas()
        {
            List<Mesas> Mesas = _dal.getMesas();
            List<DTMesa> dt_Mesas = new List<DTMesa>();
            foreach (Mesas m in Mesas)
            {
                dt_Mesas.Add(_cas.getDTMesa(m));
            }

            return dt_Mesas;
        }

        public MensajeRetorno Modificar_Mesa(DTMesa dtm)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtm != null)
            {
                if (_dal.modificar_Mesas(dtm) == true)
                {
                    men.El_Ingrediente_se_guardo_Correctamente();
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

        public MensajeRetorno baja_Mesa(int id)
        {
            throw new NotImplementedException();
        }

        public byte[] cerarMesa(DTMesa modificar)
        {
            byte[] pdf_Pedido = _fu.pdfPedido(modificar.id_Mesa);
            return pdf_Pedido;
        }
    }
}
