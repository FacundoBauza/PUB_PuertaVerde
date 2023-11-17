using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class MesaController : Controller
    {

        private readonly IB_Mesa bl;
        public MesaController(IB_Mesa _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarMesa")]
        [Authorize(Roles = "ADMIN, MOZO, CAJA")]
        public IActionResult Post([FromBody] DTMesa value)
        {
            MensajeRetorno mensajeRetorno = bl.Agregar_Mesa(value);

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
        [HttpGet("/api/listarMesas")]
        [Authorize(Roles = "ADMIN, MOZO, CAJA")]
        public List<DTMesa> Get()
        {
            return bl.Listar_Mesas();
        }

        //Eliminar
        [HttpDelete("/api/bajaMesa/{id:int}")]
        [Authorize(Roles = "ADMIN, MOZO, CAJA")]
        public IActionResult BajaMesa(int id)
        {
            MensajeRetorno mensajeRetorno = bl.Baja_Mesa(id);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //Modificar
        [HttpPut("/api/modificarMesa")]
        [Authorize(Roles = "ADMIN, MOZO, CAJA")]
        public IActionResult Put([FromBody] DTMesa Modificar)
        {
            MensajeRetorno mensajeRetorno = bl.Modificar_Mesa(Modificar);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //Modificar
        [HttpPut("/api/modificarprecio")]
        [Authorize(Roles = "ADMIN, MOZO, CAJA")]
        public IActionResult PutPrecio([FromBody] DTMesa Modificar)
        {
            MensajeRetorno mensajeRetorno = bl.Modificar_Precio_Mesa(Modificar);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //Cerar cuenta de la mesa
        [HttpPut("/api/cerarCuentaMesa")]
        [Authorize(Roles = "ADMIN, MOZO, CAJA")]
        public ActionResult<byte[]> CerarMesa([FromBody] DTMesa Modificar)
        {
            return bl.CerarMesa(Modificar);
        }

        [HttpGet("/api/agregarPagoParcial/{id:int}/{pagoEfectuado:float}")]
        [Authorize(Roles = "ADMIN, MOZO, CAJA")]
        public IActionResult AgregarPagoParcial(int id, float pagoEfectuado)
        {
            MensajeRetorno mensajeRetorno = bl.AgregarPagoParcial(id, pagoEfectuado);
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
