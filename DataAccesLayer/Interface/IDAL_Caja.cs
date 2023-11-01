using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interface
{
    public interface IDAL_Caja
    {
        //Agregar
        bool set_Categoria(DTCaja dtc);

        //Listar
        List<Cajas> getCategorias();

        //Baja
        bool baja_Categoria(int id);
    }
}
