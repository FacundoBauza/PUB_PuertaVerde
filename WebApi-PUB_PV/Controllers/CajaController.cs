using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CajaController : Controller
    {
        private readonly IB_Caja bl;

        public CajaController(IB_Caja bl)
        {
            this.bl = bl;
        }
        //Agregar
        [HttpPost("/api/agregarCaja")]
        [Authorize(Roles = "CAJA")]
        public IActionResult Post([FromBody] DTCaja value)
        {
            MensajeRetorno mensajeRetorno = bl.Set_Cajas(value);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //modificar
        [HttpPut("/api/modificarCaja")]
        [Authorize(Roles = "CAJA")]
        public IActionResult Put([FromBody] DTCaja Modificar)
        {
            MensajeRetorno mensajeRetorno = bl.Modificar_Cajas(Modificar);
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
        [HttpGet("/api/listarCaja")]
        [Authorize(Roles = "CAJA")]
        public List<DTCaja> Get()
        {
            return bl.GetCajas();
        }

        //Listar activa
        [HttpGet("/api/listarCajaavtiva")]
        [Authorize(Roles = "CAJA")]
        public List<DTCaja> Getactivas()
        {
            return bl.GetCajasactivas();
        }
        //Eliminar
        [HttpDelete("/api/bajaCaja/{id:int}")]
        [Authorize(Roles = "CAJA")]
        public IActionResult BajaCaja(int id)
        {
            MensajeRetorno mensajeRetorno = bl.Baja_Cajas(id);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        [HttpGet("/api/sumarPrecioCaja/{precio:float}")]
        [Authorize(Roles = "CAJA")]
        public IActionResult SumarPrecioCaja(float precio)
        {
            MensajeRetorno mensajeRetorno = bl.SumarPrecioCaja(precio);
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        [HttpGet("/api/cerrarCajaActiva")]
        [Authorize(Roles = "CAJA")]
        public IActionResult CerrarCajaActiva()
        {
            MensajeRetorno mensajeRetorno = bl.CerrarCajaActiva();
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
