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
    public class CategoriaController : Controller
    {
        private readonly IB_Categoria bl;
        public CategoriaController(IB_Categoria _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarCategoria")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Post([FromBody] DTCategoria value)
        {
            MensajeRetorno mensajeRetorno = bl.agregar_Categoria(value);
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
        [HttpGet("/api/listarCategorias")]
        [Authorize(Roles = "ADMIN")]
        public List<DTCategoria> Get()
        {
            return bl.listar_Categoria();
        }

        //Eliminar
        [HttpDelete("/api/bajaCategoria/{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public IActionResult BajaCategoria(int id)
        {
            MensajeRetorno mensajeRetorno = bl.baja_Categoria(id);
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
