using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_Usuario: IDAL_Usuario
    {
        private readonly DataContext _db;
        public DAL_Usuario(DataContext db)
        {
            _db = db;
        }


        //Agregar => Etapa: Sin Empezar
        bool IDAL_Usuario.add_Usuario(Usuarios t)
        {
            _db.Users.Add(t);
            return true;
        }
    }
}
