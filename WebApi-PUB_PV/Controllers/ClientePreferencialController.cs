using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class ClientePreferencialController : Controller
    {
        private IB_ClientePreferencial bl;
        public ClientePreferencialController(IB_ClientePreferencial _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarCliente")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Post([FromBody] DTCliente_Preferencial value)
        {
            MensajeRetorno mensajeRetorno = bl.agregar_ClientePreferencial(value);
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
        [HttpPut("/api/actualizarCliente")]
        [Authorize(Roles = "ADMIN, CAJA")]
        public IActionResult Put([FromBody] DTCliente_Preferencial value)
        {
            MensajeRetorno mensajeRetorno = bl.actualizar_ClientePreferencial(value);
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
        [HttpGet("/api/listarCliente")]
        [Authorize(Roles = "ADMIN, CAJA, MOZO")]
        public List<DTCliente_Preferencial> Get()
        {
            return bl.listar_ClientePreferencial();
        }

        ///Eliminar
        [HttpDelete("/api/bajaCliente/{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult BajaCliente(int id)
        {
            MensajeRetorno mensajeRetorno = bl.baja_ClientePreferencial(id);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //Cerar cuenta del Cliente Preferencial
        [HttpPut("/api/cerarCuentaCliente")]
        [Authorize(Roles = "ADMIN, CAJA")]
        public byte[] cerarCuenta([FromBody] DTCliente_Preferencial Modificar)
        {
            return bl.cerarCuenta(Modificar);
        }
    }
}
