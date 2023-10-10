using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class PedidoController : Controller
    {
        private IB_Pedido bl;
        public PedidoController(IB_Pedido _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarPedido")]
        public ActionResult<DTPedido> Post([FromBody] DTPedido value)
        {
            MensajeRetorno x = bl.agregar_Pedido(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Actualizar    
        [HttpPut("/api/actualizarPedido")]
        public ActionResult<DTPedido> Put([FromBody] DTPedido value)
        {
            MensajeRetorno x = bl.actualizar_Pedido(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/listarPedidos")]
        public List<DTPedido> Get()
        {
            return bl.listar_Pedidos();
        }

        //Listar Activos
        [HttpGet("/api/listarPedidosActivos")]
        public List<DTPedido> GetActivos()
        {
            return bl.listar_PedidosActivos();
        }

        ///Eliminar
        [HttpDelete("/api/bajaPedido/{id:int}")]
        public ActionResult<bool> BajaPedido(int id)
        {
            MensajeRetorno x = bl.baja_Pedido(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
        /*
        [HttpGet("/api/pdfPedido/{id:int}")]
        public ActionResult<byte[]> Getpdf(int id)
        {
            return bl.pdf_Pedido(id);
            //return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }*/
    }
}
