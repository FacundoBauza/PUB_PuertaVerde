﻿using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_Categoria: IDAL_Categoria
    {
        private readonly DataContext _db;
        public DAL_Categoria(DataContext db)
        {
            _db = db;
        }

        //Agregar
        bool IDAL_Categoria.set_Categoria(DTCategoria dtc)
        {
            Categorias aux = Categorias.SetCategoria(dtc);
            try
            {
                _db.Categorias.Add(aux);
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Listar
        List<Categorias> IDAL_Categoria.getCategorias() {
            return _db.Categorias.Select(x => x.GetCategoria()).ToList();
        }
    }
}
