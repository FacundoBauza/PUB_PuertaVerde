using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
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
        public ActionResult<DTIngrediente> Post([FromBody] DTIngrediente value)
        {
            MensajeRetorno x = bl.Agregar_Ingrediente(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/listarIngredientes")]
        public List<DTIngrediente> Get()
        {
            return bl.Listar_Ingrediente();
        }

        //Eliminar
        [HttpDelete("/api/bajaIngrediente/{id:int}")]
        public ActionResult<MensajeRetorno> Delete(int id)
        {
            return bl.Eliminar_Ingredente(id);
        }

        //Modificar
        [HttpPut("/api/modificarIngrediente")]
        public ActionResult<DTIngrediente> Put([FromBody] DTIngrediente Modificar)
        {
            MensajeRetorno x = bl.Modificar_Ingrediente(Modificar);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
    }
}