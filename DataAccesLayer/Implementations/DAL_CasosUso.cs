using DataAccesLayer.Interface;

namespace DataAccesLayer.Implementations
{
    public class DAL_CasosUso : IDAL_CasosUso
    {
        private readonly DataContext _db;
        public DAL_CasosUso(DataContext db)
        {
            _db = db;
        }
    }
}
