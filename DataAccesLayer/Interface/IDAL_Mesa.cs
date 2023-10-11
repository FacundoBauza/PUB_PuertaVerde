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
        List<Mesas> getMesas();
        bool modificar_Mesas(DTMesa dtm);
        bool set_Mesa(DTMesa dtm);
    }
}
