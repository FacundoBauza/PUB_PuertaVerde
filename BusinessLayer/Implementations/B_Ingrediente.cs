using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;

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
                        men.mensaje = "El Ingrediente se guardo correctamente";
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
                    men.mensaje = "Ya existe un Ingrediente con el Nombre ingresado";
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

        public MensajeRetorno Eliminar_Ingredente(int id)
        {
            return _dal.Eliminar_Ingredente(id);
        }

        public List<DTIngrediente> Listar_Ingrediente()
        {
            List<Ingredientes> Ingredientes = _dal.getIngrediente();
            List<DTIngrediente> dt_Ingredientes = new List<DTIngrediente>();
            foreach (Ingredientes c in Ingredientes)
            {
                dt_Ingredientes.Add(_cas.GetDTIngrediente(c));
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
                    men.mensaje = "El Ingrediente se modifico correctamente";
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
    }
}
