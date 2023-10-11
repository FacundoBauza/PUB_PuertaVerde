using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Implementations
{
    public class DAL_ClientePreferencial : IDAL_ClientePreferencial
    {
        private readonly DataContext _db;
        public DAL_ClientePreferencial(DataContext db)
        {
            _db = db;
        }

        //Agregar
        bool IDAL_ClientePreferencial.set_Cliente(DTCliente_Preferencial dtCP)
        {
            ClientesPreferenciales aux = ClientesPreferenciales.SetCliente(dtCP);
            try
            {
                _db.ClientesPreferenciales.Add(aux);
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }

        //Actualizar
        bool IDAL_ClientePreferencial.update_Cliente(DTCliente_Preferencial dtCP)
        {
            ClientesPreferenciales? aux = null;
            aux = _db.ClientesPreferenciales.FirstOrDefault(cli => cli.id_Cli_Preferencial == dtCP.id_Cli_Preferencial);
            if (aux != null)
            {
                aux.nombre = dtCP.nombre;
                aux.apellido = dtCP.apellido;
                aux.telefono = dtCP.telefono;
                aux.saldo = dtCP.saldo;
                aux.fichasCanje = dtCP.fichasCanje;
                try
                {
                    _db.Update(aux);
                    _db.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        //Listar
        List<ClientesPreferenciales> IDAL_ClientePreferencial.get_Cliente()
        {
            return _db.ClientesPreferenciales.Where(x => x.registro_Activo).Select(x => x.GetCliente()).ToList();
        }

        //Baja 
        bool IDAL_ClientePreferencial.baja_Cliente(int id)
        {
            ClientesPreferenciales? aux = null;
            aux = _db.ClientesPreferenciales.FirstOrDefault(cli => cli.id_Cli_Preferencial == id);
            if (aux != null)
            {
                try
                {
                    aux.registro_Activo = false;
                    _db.Update(aux);
                    _db.SaveChanges();
                }
                catch
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
