using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Mesa
    {
        List<Mesas> GetMesas();
        bool Modificar_Mesas(DTMesa dtm);
        bool Set_Mesa(DTMesa dtm);
        byte[] CerarMesa(int id);
    }
}
