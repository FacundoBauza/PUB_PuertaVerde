using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using DataAccesLayer.Interface;
using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics.X86;

namespace DataAccesLayer.Implementations
{
    public class DAL_Mesa : IDAL_Mesa
    {
        private readonly DataContext _db;
        public DAL_Mesa(DataContext db)
        {
            _db = db;
        }

        public List<Mesas> getMesas()
        {
            return _db.Mesas.Select(x => x.GetMesa()).ToList();
        }

        public bool modificar_Mesas(DTMesa dtm)
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
                    // Guarda los cambios en la base de datos.
                    _db.Update(MesaEncontrada);
                    _db.SaveChanges();
                    //retota que todo se hizo corectamente
                    return true;
                }
                catch { }
            }
            //no se pudo encontrar la mesa y retorna false
            return false;
        }

        public bool set_Mesa(DTMesa dtm)
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
        public byte[] cerarMesa(int id)
        {
            //string para con todo el contenido para luego cargarselo al pdf
            string factura = "";
            //total
            float total = 0;
            //PDF en binario
            byte[] pdfData;
#pragma warning disable CS8604 // Posible argumento de referencia nulo
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
                    aux.estadoProceso = false;
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
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            Mesas? mesa = _db.Mesas.SingleOrDefault(i => i.id_Mesa == id);
            //Dejo la mesa libre
            mesa.precioTotal = 0;
            mesa.enUso = false;
            _db.Mesas.Update(mesa);
            _db.SaveChanges();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            //retorno el pdf
            return pdfData;
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
