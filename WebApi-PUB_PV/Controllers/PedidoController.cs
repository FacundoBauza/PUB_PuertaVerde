using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalR;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class PedidoController : Controller
    {
        private IB_Pedido bl;
        private IHubContext<ChatHub> _hub;
        public PedidoController(IB_Pedido _bl, IHubContext<ChatHub> hub)
        {
            bl = _bl;
            _hub = hub;
        }

        //Agregar
        [HttpPost("/api/agregarPedido")]
        public async Task<ActionResult<DTPedido>> Post([FromBody] DTPedido value)
        {
            MensajeRetorno x = bl.agregar_Pedido(value);
            if (value.tipo == Domain.Enums.Categoria.comida || value.tipo == Domain.Enums.Categoria.licuado)
            {
                await _hub.Clients.All.SendAsync("NewPedido", "Pedido", "Se ha actualizado un pedido");
            }
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Actualizar    
        [HttpPut("/api/actualizarPedido")]
        public ActionResult<DTPedido> Put([FromBody] DTPedido value)
        {
            MensajeRetorno x = bl.actualizar_Pedido(value);
            if(value.tipo == Domain.Enums.Categoria.comida || value.tipo == Domain.Enums.Categoria.licuado)
            {
                _hub.Clients.All.SendAsync("ClosePedido", "Pedido de la mesa"+value.id_Mesa+" terminado");
            }   
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/Pedido{id:int}")]
        public DTPedido GetPedido(int id)
        {
            return bl.Pedido(id);
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

        //Listar
        [HttpGet("/api/listarPedidosPorTipo{tipo}")]
        public List<DTPedido> GetPedidosPorTipo(Domain.Enums.Categoria tipo)
        {
            return bl.listar_PedidosPorTipo(tipo);
        }

        //Listar
        [HttpGet("/api/listarPedidosPorMesa{id}")]
        public List<DTPedido> GetPedidosPorMesa(int id)
        {
            return bl.listar_PedidosPorMesa(id);
        }

        ///Eliminar
        [HttpPost("/api/finalizarPedido/{id:int}")]
        public ActionResult<bool> finalizarPedido(int id)
        {
            MensajeRetorno x = bl.finalizar_Pedido(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        ///Eliminar
        [HttpDelete("/api/bajaPedido/{id:int}")]
        public ActionResult<bool> BajaPedido(int id)
        {
            MensajeRetorno x = bl.baja_Pedido(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
    }
}
