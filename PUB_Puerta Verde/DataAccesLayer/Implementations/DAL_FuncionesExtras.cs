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

        public byte[] pdfPedido(int id)
        {
            //string para con todo el contenido para luego cargarselo al pdf
            //lista para guardar los productos_pedidos
            string factura = "";
            List<Pedidos_Productos> pp = new();
            //total
            float total = 0;
            //PDF en binario
            byte[] pdfData;
            //Traigo todos los pedidos sin pagar de esa mesa
            List<Pedidos> p = _db.Pedidos.Where(x => x.id_Mesa == id & x.pago == false).Select(x => x.GetPedido()).ToList();
            //Recorro todos los pedidos
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            foreach (Pedidos Pedido in p)
            {
                //Traigo los productos que tiene ese pedido
                pp = _db.Pedidos_Productos.Where(x => x.id_Pedido == Pedido.id_Pedido).Select(x => x.GetPedidos_Productos()).ToList();
                foreach (Pedidos_Productos Pepr in pp)
                {
                    //Me traigo el producto
                    Productos? producto = _db.Productos.SingleOrDefault(i => i.id_Producto == Pepr.id_Producto);
                    if (producto != null)
                    {
                        //Agrego el producto a la fatura
                        factura += Environment.NewLine + producto.nombre + " " + producto.precio;
                        //Sumo los precios
                        total += producto.precio;
                    }
                }
                Pedidos? aux = _db.Pedidos.FirstOrDefault(pe => pe.id_Pedido == Pedido.id_Pedido);
                if (aux != null)
                {
                    aux.pago = true;
                    _db.Pedidos.Update(aux);
                    _db.SaveChanges();
                }
            }
            factura += Environment.NewLine + "      TOTAL: " + total;
#pragma warning restore CS8604
            using (MemoryStream ms = new MemoryStream())
            {
                // Crea un nuevo documento PDF
                using (var pdfDoc = new PdfDocument(new PdfWriter(ms)))
                {
                    // Crea un nuevo documento PDF vacío
                    using (var document = new Document(pdfDoc))
                    {
                        // Agrega el contenido al documento
                        Paragraph paragraph = new Paragraph(factura);
                        document.Add(paragraph);
                    }
                }

                // Convierte el MemoryStream en un arreglo de bytes
                pdfData = ms.ToArray();
            }
            //Traigo la mesa
            Mesas? mesa = _db.Mesas.SingleOrDefault(i => i.id_Mesa == id);
            if (mesa != null)
            {
                //Dejo la mesa libre
                mesa.precioTotal = 0;
                mesa.enUso = false;
            }
            //retorno el pdf
            return pdfData;
        }
    }
}
