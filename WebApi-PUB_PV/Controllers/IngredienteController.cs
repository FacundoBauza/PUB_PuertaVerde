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
    public class IngredienteController : Controller
    {
        private IB_Ingrediente bl;
        public IngredienteController(IB_Ingrediente _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarIngrediente")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Post([FromBody] DTIngrediente value)
        {
            MensajeRetorno mensajeRetorno = bl.Agregar_Ingrediente(value);
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
        [HttpGet("/api/listarIngredientes")]
        [Authorize(Roles = "ADMIN")]
        public List<DTIngrediente> Get()
        {
            return bl.Listar_Ingrediente();
        }

        //Eliminar
        [HttpDelete("/api/bajaIngrediente/{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            MensajeRetorno mensajeRetorno = bl.Eliminar_Ingredente(id);
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
        [HttpPut("/api/modificarIngrediente")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Put([FromBody] DTIngrediente Modificar)
        {
            MensajeRetorno mensajeRetorno = bl.Modificar_Ingrediente(Modificar);
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