using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Interface
{
    public interface IDAL_ClientePreferencial
    {
        //Agregar
        bool set_Cliente(DTCliente_Preferencial dtCP);
        //Actualizar
        bool update_Cliente(DTCliente_Preferencial dtCP);
        //Listar
        List<ClientesPreferenciales> get_Cliente();
        //Baja
        bool baja_Cliente(int id);
    }
}
