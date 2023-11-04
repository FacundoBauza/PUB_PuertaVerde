using DataAccesLayer.Interface;
using DataAccesLayer.Models;

namespace DataAccesLayer.Implementations
{
    public class DAL_Usuario : IDAL_Usuario
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
