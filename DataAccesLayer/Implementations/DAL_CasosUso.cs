using DataAccesLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_CasosUso: IDAL_CasosUso
    {
        private readonly DataContext _db;
        public DAL_CasosUso(DataContext db)
        {
            _db = db;
        }
    }
}
