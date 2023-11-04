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
            // Crear un MemoryStream para almacenar el PDF en memoria.
            using (MemoryStream stream = new MemoryStream())
            {
                // Crear un nuevo documento PDF y asociarlo con el MemoryStream.
                using (var pdfDoc = new PdfDocument(new PdfWriter(stream)))
                {
                    // Crear un documento iText7 para agregar contenido al PDF.
                    using (var document = new Document(pdfDoc))
                    {

                        // Ajusta el tamaño del papel
                        //PageSize pageSize = new PageSize(80, 300); //me rompe todo voy a ver como se imprime sin eso y luego veo
                        //pdfDoc.SetDefaultPageSize(pageSize);

                        // Define el formato de texto
                        PdfFont font = PdfFontFactory.CreateFont("Helvetica");
                        float fontSize = 10;

                        // Crea el título del ticket
                        Paragraph title = new Paragraph("La Puerta Verde Open Bar")
                            .SetFont(font)
                            .SetFontSize(fontSize + 4)
                            .SetBold()
                            .SetTextAlignment(TextAlignment.CENTER);
                        document.Add(title);

                        // Crea una tabla para la lista de productos o servicios
                        Table table = new Table(3);
                        table.SetWidth(UnitValue.CreatePercentValue(100));

                        // Define las celdas de la tabla
                        Cell cell1 = new Cell().Add(new Paragraph("Producto"))
                            .SetFont(font)
                            .SetFontSize(fontSize)
                            .SetBold()
                            .SetTextAlignment(TextAlignment.LEFT);
                        Cell cell2 = new Cell().Add(new Paragraph("Cantidad"))
                            .SetFont(font)
                            .SetFontSize(fontSize)
                            .SetBold()
                            .SetTextAlignment(TextAlignment.CENTER);
                        Cell cell3 = new Cell().Add(new Paragraph("Total"))
                            .SetFont(font)
                            .SetFontSize(fontSize)
                            .SetBold()
                            .SetTextAlignment(TextAlignment.RIGHT);

                        // Agrega las celdas a la tabla
                        table.AddCell(cell1);
                        table.AddCell(cell2);
                        table.AddCell(cell3);

                        // Agrega los elementos al ticket

                        List<DTPDF> PDF = new();
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
                                    //me fijo si el producto ya esta en la factura
                                    foreach (DTPDF item in PDF)
                                    {
                                        if (item.nombre.Equals(producto.nombre))
                                            //si esta suno 1 a cantidad
                                            item.cantidad++;
                                        else
                                        {
                                            //Agrego el producto
                                            DTPDF aux1 = new(producto.nombre, 1, producto.precio);
                                            PDF.Add(aux1);
                                        }
                                    }
                                }
                            }
                            Pedidos? aux = _db.Pedidos.FirstOrDefault(pe => pe.id_Pedido == Pedido.id_Pedido);
                            if (aux != null)
                            {
                                aux.pago = true;
                                aux.estadoProceso = false;
                                _db.Pedidos.Update(aux);
                                _db.SaveChanges();
                            }
                        }

                        // Calcula y muestra el total
                        float totalVenta = 0;

                        for (int i = 0; i < PDF.Count; i++)
                        {
                            table.AddCell(new Paragraph(PDF[i].nombre).SetFont(font).SetFontSize(fontSize).SetTextAlignment(TextAlignment.LEFT));
                            table.AddCell(new Paragraph(PDF[i].cantidad.ToString()).SetFont(font).SetFontSize(fontSize).SetTextAlignment(TextAlignment.CENTER));
                            table.AddCell(new Paragraph((PDF[i].precio * PDF[i].cantidad).ToString()).SetFont(font).SetFontSize(fontSize).SetTextAlignment(TextAlignment.RIGHT));
                            totalVenta += PDF[i].precio * PDF[i].cantidad;
                        }

                        // Controla el flujo de texto para evitar desbordamientos
                        table.SetKeepTogether(true);
                        document.Add(table);


                        Paragraph total = new Paragraph("Total: " + totalVenta.ToString())
                            .SetFont(font)
                            .SetFontSize(fontSize)
                            .SetTextAlignment(TextAlignment.RIGHT);
                        document.Add(total);
                    }
                }
                // Convierte el PDF en un arreglo de bytes
                //byte[] pdfBytes = stream.ToArray();
                //Retorna el pdf
                //return Convert.ToBase64String(pdfBytes);
                return stream.ToArray();
            }
        }

    }
}