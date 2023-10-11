using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : Controller
    {
        private IB_Categoria bl;
        public CategoriaController(IB_Categoria _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarCategoria")]
        public ActionResult<DTCategoria> Post([FromBody] DTCategoria value)
        {
            MensajeRetorno x = bl.agregar_Categoria(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/listarCategorias")]
        public List<DTCategoria> Get()
        {
            return bl.listar_Categoria();
        }

        //Eliminar
        [HttpDelete("/api/bajaCategoria/{id:int}")]
        public ActionResult<bool> BajaCategoria(int id)
        {
            MensajeRetorno x = bl.baja_Categoria(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
    }
}
