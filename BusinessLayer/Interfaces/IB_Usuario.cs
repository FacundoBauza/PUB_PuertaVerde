using DataAccesLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_Usuario
    {
        //Agregar
        bool agregar_Usuario(Usuarios t);
    }
}
