using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

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

        public byte[] cerarCuenta(int id)
        {
            //string para con todo el contenido para luego cargarselo al pdf
            string factura = "";
            //total
            float total = 0;
            //PDF en binario
            byte[] pdfData;
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            //Traigo todos los pedidos sin pagar de este cliente y los recorro
            foreach (Pedidos Pedido in _db.Pedidos.Where(x => x.id_Cli_Preferencial == id & x.pago == false).Select(x => x.GetPedido()).ToList())
            {
                //Traigo los productos que tiene ese pedido y los recorro
                foreach (Pedidos_Productos Pepr in _db.Pedidos_Productos.Where(x => x.id_Pedido == Pedido.id_Pedido).Select(x => x.GetPedidos_Productos()).ToList())
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
                // Convierte el PDF en un arreglo de bytes
                //byte[] pdfBytes = stream.ToArray();
                //Retorna el pdf
                //return Convert.ToBase64String(pdfBytes);
                return stream.ToArray();
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
                //falta ver que se hace con el saldo del cliente 
                // Convierte el MemoryStream en un arreglo de bytes
                pdfData = ms.ToArray();
            }

            //retorno el pdf
            return pdfData;
        }

    }
}
