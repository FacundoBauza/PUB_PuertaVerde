using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
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
        public List<DTCliente_Preferencial> Get()
        {
            return bl.listar_ClientePreferencial();
        }

        ///Eliminar
        [HttpDelete("/api/bajaCliente/{id:int}")]
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
        public ActionResult<byte[]> cerarCuenta([FromBody] DTCliente_Preferencial Modificar)
        {
            return bl.cerarCuenta(Modificar);
        }
    }
}
