﻿using BusinessLayer.Interfaces;
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

namespace BusinessLayer.Implementations
{
    public class B_Estadisticas : IB_Estadisticas
    {
        private IDAL_Estadisticas _dal;
        private IDAL_Casteo _cas;
        private IDAL_FuncionesExtras _fu;

        public B_Estadisticas(IDAL_Estadisticas dal, IDAL_Casteo cas, IDAL_FuncionesExtras fu)
        {
            _dal = dal;
            _cas = cas;
            _fu = fu;
        }

        public DTProductoEstadistica producto(DTProductoEstadistica value)
        {
            return _dal.producto(value);
        }

        public List<DTProductoEstadistica> productostipo(DTProductoEstadistica value)
        {
            throw new NotImplementedException();
        }

        public List<DTProductoEstadistica> todoslosproductos(DTProductoEstadistica value)
        {
            throw new NotImplementedException();
        }
    }
}