using System;
using System.IO;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Domain.Entidades;

namespace DataAccesLayer.Implementations
{
    public class DAL_FuncionesExtras : IDAL_FuncionesExtras
    {
        private readonly DataContext _db;
        private IDAL_Casteo _cas;
        public DAL_FuncionesExtras(DataContext db, IDAL_Casteo cas)
        {
            _db = db;
            _cas = cas;
        }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public DAL_FuncionesExtras()
        {
        }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.

        bool IDAL_FuncionesExtras.existeCategoria(string nombre)
        {
            if (_db.Categorias.SingleOrDefault(i => i.nombre == nombre) != null)
                return true;
            return false;
        }

        bool IDAL_FuncionesExtras.existeCliente(string telefono)
        {
            if (_db.ClientesPreferenciales.SingleOrDefault(i => i.telefono == telefono) != null)
                return true;
            return false;
        }

        bool IDAL_FuncionesExtras.existeIngrediente(string nombre)
        {
            // Utiliza SingleOrDefault() para buscar un ingrediente por nombre.
            if (_db.Ingredientes.SingleOrDefault(i => i.nombre == nombre) != null)
                return true;
            return false;
        }

        bool IDAL_FuncionesExtras.existeClienteId(int id)
        {
            if (_db.ClientesPreferenciales.SingleOrDefault(i => i.id_Cli_Preferencial == id) != null)
                return true;
            return false;
        }

        public bool existeProducto(string nombre)
        {
            // Utiliza SingleOrDefault() para buscar un producto por nombre.
            if (_db.Productos.SingleOrDefault(i => i.nombre == nombre) != null)
                return true;
            return false;
        }

        public bool existeMesa(int id_Mesa)
        {
            // Utiliza SingleOrDefault() para buscar una Mesa.
            if (_db.Mesas.SingleOrDefault(i => i.id_Mesa == id_Mesa) != null)
                return true;
            return false;
        }

        public bool existePedido(int id_Pedido)
        {
            if (_db.Pedidos.SingleOrDefault(i => i.id_Pedido == id_Pedido) != null)
                return true;
            return false;
        }

        public bool existeUsuario(string username)
        {
            if (_db.Users.SingleOrDefault(i => i.UserName == username) != null)
                return true;
            return false;
        }

        public bool mesaEnUso(int idMesa)
        {
            Mesas? aux = _db.Mesas.SingleOrDefault(i => i.id_Mesa == idMesa);
            if (aux != null && aux.enUso == false)
                return false;
            return true;
        }

        public void agregarPrecioaMesa(float valor, int idMesa)
        {
            Mesas? aux = _db.Mesas.SingleOrDefault(i => i.id_Mesa == idMesa);
            if (aux != null)
            {
                aux.precioTotal = valor;
                _db.Mesas.Update(aux);
                _db.SaveChanges();
            }
        }
    }
}
