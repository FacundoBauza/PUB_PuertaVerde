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

        public DAL_FuncionesExtras()
        {
        }

        bool IDAL_FuncionesExtras.existeCategoria(string nombre)
        {
            if (_db.Categorias.Any())
            {
                foreach (Categorias x in _db.Categorias)
                {
                    if (x.nombre.Equals(nombre))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
                return false;
        }

        bool IDAL_FuncionesExtras.existeCliente(string telefono)
        {
            if (_db.ClientesPreferenciales.Any())
            {
                foreach (ClientesPreferenciales x in _db.ClientesPreferenciales)
                {
                    if (x.telefono.Equals(telefono))
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }

        bool IDAL_FuncionesExtras.existeIngrediente(string nombre)
        {
            // Utiliza SingleOrDefault() para buscar un ingrediente por nombre.
            if (_db.Ingredientes.SingleOrDefault(i => i.nombre == nombre) != null)
            {
                return true;
            }
            else
                return false;
        }

        bool IDAL_FuncionesExtras.existeClienteId(int id)
        {
            if (_db.ClientesPreferenciales.SingleOrDefault(i => i.id_Cli_Preferencial == id) != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool existeProducto(string nombre)
        {
            // Utiliza SingleOrDefault() para buscar un producto por nombre.
            if (_db.Productos.SingleOrDefault(i => i.nombre == nombre) != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool existeMesa(int id_Mesa)
        {
            // Utiliza SingleOrDefault() para buscar una Mesa.
            if (_db.Mesas.SingleOrDefault(i => i.id_Mesa == id_Mesa) != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool existePedido(int id_Pedido)
        {
            if (_db.Pedidos.SingleOrDefault(i => i.id_Pedido == id_Pedido) != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool existeUsuario(string username)
        {
            if (_db.Users.SingleOrDefault(i => i.UserName == username) != null)
            {
                return true;
            }
            else
                return false;
        }

        public bool mesaEnUso(int idMesa)
        {
            Mesas aux = _db.Mesas.SingleOrDefault(i => i.id_Mesa == idMesa);
            if (aux != null && aux.enUso == false)
            {
                return false;
            }
            else
            { 
                return true;
            }
        }

        public void agregarPrecioaMesa(float valor, int idMesa)
        {
            Mesas aux = _db.Mesas.SingleOrDefault(i => i.id_Mesa == idMesa);
            aux.precioTotal = valor;
            
            _db.Mesas.Update(aux);
            _db.SaveChanges();
        }

        public byte[] pdfPedido(int id)
        {
            //PDF en binario

            byte[] pdfData;

            using (MemoryStream ms = new MemoryStream())
            {
                // Crea un nuevo documento PDF
                using (var pdfDoc = new PdfDocument(new PdfWriter(ms)))
                {
                    // Crea un nuevo documento PDF vacío
                    using (var document = new Document(pdfDoc))
                    {
                        // Agrega contenido al documento
                        Paragraph paragraph = new Paragraph("¡Hola, este es el pdf con id "+id+" !");
                        document.Add(paragraph);
                    }
                }

                // Convierte el MemoryStream en un arreglo de bytes
                pdfData = ms.ToArray();
            }
            //retorno el pdf
            return pdfData;
            // Aquí puedes trabajar con pdfData, que contiene el PDF en forma de arreglo de bytes
            // Por ejemplo, puedes guardar el arreglo de bytes en un archivo o en base de datos , enviarlo por correo electrónico, etc.
            
            /* ejemplo de como guardarlo en la ubicación del directorio de trabajo actual (carpeta WebApi-PUB_PV en este caso)
            // Obtiene la ubicación del directorio de trabajo actual
            string workingDirectory = Environment.CurrentDirectory;

            // Nombre del archivo PDF que deseas crear
            string pdfFileName = "Pedido " + id + ".pdf";

            // Construye la ruta completa del archivo PDF
            string pdfFilePath = Path.Combine(workingDirectory, pdfFileName);


            // Crea un nuevo documento PDF
            using (var pdfDoc = new PdfDocument(new PdfWriter(pdfFilePath)))
            {
                // Crea un nuevo documento PDF vacío
                using (var document = new Document(pdfDoc))
                {
                    // Agrega contenido al documento
                    Paragraph paragraph = new Paragraph("¡Hola, mundo!");
                    document.Add(paragraph);
                }
            }
            Console.WriteLine("PDF creado con éxito en: " + pdfFilePath);
            */
        }
    }
}
