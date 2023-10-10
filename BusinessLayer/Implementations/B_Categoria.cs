using BusinessLayer.Interfaces;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementations
{
    public class B_Categoria : IB_Categoria
    {
        private IDAL_Categoria _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Categoria(IDAL_Categoria dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }

        //Agregar
        MensajeRetorno IB_Categoria.agregar_Categoria(DTCategoria dtc)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (dtc != null)
            {
                if (!_fu.existeCategoria(dtc.nombre))
                {
                    if (_dal.set_Categoria(dtc) == true)
                    {
                        men.La_Categoria_se_guardo_Correctamente();
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
                    men.Ya_existe_una_Categoria_con_el_Nombre_ingresado();
                    return men;
                }
            }
            else
            {
                men.Objeto_Nulo();
                return men;
            }

        }

        //Listar
        List<DTCategoria> IB_Categoria.listar_Categoria()
        {
            List <Categorias> categorias = _dal.getCategorias();
            List<DTCategoria> dt_categorias = new List<DTCategoria>();
            foreach (Categorias c in categorias)
            {
                dt_categorias.Add(_cas.getDTCategoria(c));
            }

            return dt_categorias;
        }

        //Baja
        MensajeRetorno IB_Categoria.baja_Categoria(int id)
        {
            MensajeRetorno men = new MensajeRetorno();
            if (_dal.baja_Categoria(id) == true)
            {
                men.La_Categoria_se_quito_Correctamente();
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
