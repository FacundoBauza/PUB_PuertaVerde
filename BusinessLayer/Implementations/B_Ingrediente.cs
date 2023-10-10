using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class B_Ingrediente : IB_Ingrediente
    {
        private IDAL_Ingrediente _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Ingrediente(IDAL_Ingrediente dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }

        public MensajeRetorno Agregar_Ingrediente(DTIngrediente dti)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dti != null)
            {
                if (!_fu.existeIngrediente(dti.nombre))
                {
                    if (_dal.set_Ingrediente(dti) == true)
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

        public List<DTIngrediente> Listar_Ingrediente()
        {
            List<Ingredientes> Ingredientes = _dal.getIngrediente();
            List<DTIngrediente> dt_Ingredientes = new List<DTIngrediente>();
            foreach (Ingredientes c in Ingredientes)
            {
                dt_Ingredientes.Add(_cas.getDTIngrediente(c));
            }

            return dt_Ingredientes;
        }

        public MensajeRetorno Modificar_Ingrediente(DTIngrediente dti)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dti != null)
            {
                if (_dal.modificar_Ingrediente(dti) == true)
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
    }
}
