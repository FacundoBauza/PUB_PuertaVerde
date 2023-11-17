using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "MOZO, CAJA")]
        public async Task<IActionResult> Post([FromBody] DTPedido value)
        {
            MensajeRetorno mensajeRetorno = bl.agregar_Pedido(value);
            if (value.tipo == Domain.Enums.Categoria.comida || value.tipo == Domain.Enums.Categoria.licuado)
            {
                await _hub.Clients.All.SendAsync("NewPedido", "Pedido", "Se ha actualizado un pedido");
            }
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //Actualizar    
        [HttpPut("/api/actualizarPedido")]
        [Authorize(Roles = "MOZO, CAJA, COCINA")]
        public IActionResult Put([FromBody] DTPedido value)
        {
            MensajeRetorno mensajeRetorno = bl.actualizar_Pedido(value);
            if(value.tipo == Domain.Enums.Categoria.comida || value.tipo == Domain.Enums.Categoria.licuado)
            {
                _hub.Clients.All.SendAsync("ClosePedido", "Pedido de la mesa"+value.id_Mesa+" terminado");
            }
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //Listar
        [HttpGet("/api/Pedido{id:int}")]
        [Authorize(Roles = "MOZO, CAJA ,COCINA")]
        public DTPedido GetPedido(int id)
        {
            return bl.Pedido(id);
        }

        //Listar
        [HttpGet("/api/listarPedidos")]
        [Authorize(Roles = "MOZO, CAJA ,COCINA")]
        public List<DTPedido> Get()
        {
            return bl.listar_Pedidos();
        }

        //Listar Activos
        [HttpGet("/api/listarPedidosActivos")]
        [Authorize(Roles = "MOZO, CAJA ,COCINA")]
        public List<DTPedido> GetActivos()
        {
            return bl.listar_PedidosActivos();
        }

        //Listar
        [HttpGet("/api/listarPedidosPorTipo{tipo}")]
        [Authorize(Roles = "MOZO, CAJA ,COCINA")]
        public List<DTPedido> GetPedidosPorTipo(Domain.Enums.Categoria tipo)
        {
            return bl.listar_PedidosPorTipo(tipo);
        }

        //Listar
        [HttpGet("/api/listarPedidosPorMesa{id}")]
        [Authorize(Roles = "MOZO, CAJA, COCINA")]
        public List<DTPedido> GetPedidosPorMesa(int id)
        {
            return bl.listar_PedidosPorMesa(id);
        }

        ///Finalizar Pedido
        [HttpPost("/api/finalizarPedido/{id:int}")]
        [Authorize(Roles = "CAJA,COCINA")]
        public IActionResult finalizarPedido(int id)
        {
            MensajeRetorno mensajeRetorno = bl.finalizar_Pedido(id);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        ///Eliminar
        [HttpDelete("/api/bajaPedido/{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult BajaPedido(int id)
        {
            MensajeRetorno mensajeRetorno = bl.baja_Pedido(id);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }
    }
}
