using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class B_ClientePreferencial: IB_ClientePreferencial
    {
        private IDAL_ClientePreferencial _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_ClientePreferencial(IDAL_ClientePreferencial dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }

        MensajeRetorno IB_ClientePreferencial.agregar_ClientePreferencial(DTCliente_Preferencial dtCP)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtCP != null)
            {
                if (!_fu.existeCliente(dtCP.telefono))
                {
                    if (_dal.set_Cliente(dtCP) == true)
                    {
                        men.El_Cliente_se_guardo_Correctamente();
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
                    men.Ya_existe_un_Cliente_con_los_datos_ingresado();
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }
        }

        MensajeRetorno IB_ClientePreferencial.actualizar_ClientePreferencial(DTCliente_Preferencial dtCP)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtCP != null)
            {
                if (_fu.existeClienteId(dtCP.id_Cli_Preferencial))
                {
                    if (_dal.update_Cliente(dtCP) == true)
                    {
                        men.El_Cliente_se_actualizo_Correctamente();
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
                    men.No_existe_un_Cliente_con_los_datos_ingresado();
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }
        }

        List<DTCliente_Preferencial> IB_ClientePreferencial.listar_ClientePreferencial()
        {
            List<DTCliente_Preferencial> clientes = new List<DTCliente_Preferencial>();
            foreach (ClientesPreferenciales x in _dal.get_Cliente())
            {
                if(x.registro_Activo == true)
                    clientes.Add(_cas.castDTCliente_Preferencial(x));
            }

            return clientes;
        }

        MensajeRetorno IB_ClientePreferencial.baja_ClientePreferencial(int id)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (_dal.baja_Cliente(id) == true)
            {
                men.El_Cliente_se_quito_Correctamente();
                return men;
            }
            else
            {
                men.Exepcion_no_Controlada();
                return men;
            }
        }
    }
}
