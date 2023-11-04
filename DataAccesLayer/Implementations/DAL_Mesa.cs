using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace DataAccesLayer.Implementations
{
    public class DAL_Mesa : IDAL_Mesa
    {
        private readonly DataContext _db;
        public DAL_Mesa(DataContext db)
        {
            _db = db;
        }

        public List<Mesas> GetMesas()
        {
            return _db.Mesas.Where(x  => x.registro_Activo).Select(x => x.GetMesa()).ToList();
        }

        public bool Modificar_Mesas(DTMesa dtm)
        {
            // Utiliza SingleOrDefault() para buscar una Mesa.
            var MesaEncontrada = _db.Mesas.SingleOrDefault(i => i.id_Mesa == dtm.id_Mesa);
            if (MesaEncontrada != null)
            {
                try
                {
                    // Modifica las propiedades de la mesa.
                    MesaEncontrada.enUso = dtm.enUso;
                    MesaEncontrada.precioTotal = dtm.precioTotal;
                    MesaEncontrada.nombre = dtm.nombre;
                    // Guarda los cambios en la base de datos.
                    _db.Mesas.Update(MesaEncontrada);
                    _db.SaveChanges();
                    //retota que todo se hizo corectamente
                    return true;
                }
                catch { }
            }
            //no se pudo encontrar la mesa y retorna false
            return false;
        }

        public bool Set_Mesa(DTMesa dtm)
        {
            //Castea el DT en tipo Mesas
            Mesas aux = Mesas.SetMesa(dtm);
            try
            {
                //Agrega la Mesas
                _db.Mesas.Add(aux);

                // Guarda los cambios en la base de datos.
                _db.SaveChanges();
            }
            catch
            {
                //si ocurrio algun error retorna false
                return false;
            }
            //todo bien y retorna true
            return true;
        }
        public byte[] CerarMesa(int id)
        {
            // Crear un MemoryStream para almacenar el PDF en memoria.
            using MemoryStream stream = new();
            // Crear un nuevo documento PDF y asociarlo con el MemoryStream.
            using (var pdfDoc = new PdfDocument(new PdfWriter(stream)))
            {
                // Crear un documento iText7 para agregar contenido al PDF.
                using var document = new Document(pdfDoc);

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
                Table table = new(3);
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
                //Traigo todos los pedidos sin pagar de esa mesa y los recorro
                foreach (Pedidos Pedido in _db.Pedidos.Where(x => x.id_Mesa == id & x.pago == false).Select(x => x.GetPedido()).ToList())
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
            //Libera la mesa
            Mesas? mesa = _db.Mesas.FirstOrDefault(m => m.id_Mesa == id);
            if (mesa != null)
            {
                mesa.enUso = false;
                mesa.precioTotal = 0;
                _db.Mesas.Update(mesa);
                _db.SaveChanges();

            }
            // Convierte el PDF en un arreglo de bytes
            //byte[] pdfBytes = stream.ToArray();
            //Retorna el pdf
            //return Convert.ToBase64String(pdfBytes);
            return stream.ToArray();
        }

        public bool Baja_Mesa(int id)
        {
            // Utiliza SingleOrDefault() para buscar una Mesa.
            var MesaEncontrada = _db.Mesas.SingleOrDefault(i => i.id_Mesa == id);
            if (MesaEncontrada != null)
            {
                try
                {
                    // Modifica las propiedades de la mesa.
                    MesaEncontrada.registro_Activo = false;
                    // Guarda los cambios en la base de datos.
                    _db.Mesas.Update(MesaEncontrada);
                    _db.SaveChanges();
                    //retota que todo se hizo corectamente
                    return true;
                }
                catch { }
            }
            //no se pudo encontrar la mesa y retorna false
            return false;
        }

        public bool baja_Mesa(int id)
        {
            Mesas? aux = null;
            aux = _db.Mesas.FirstOrDefault(me => me.id_Mesa == id);
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
