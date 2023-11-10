using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class Productos_IngredientesController : Controller
    {

        private IBProductos_Ingredientes bl;
        public Productos_IngredientesController(IBProductos_Ingredientes _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarProductos_Ingredientes")]
        public IActionResult Post([FromBody] DTProductos_Ingredientes value)
        {
            MensajeRetorno mensajeRetorno = bl.Productos_Ingredientes(value); 
            if (mensajeRetorno.status)
            {
                return Ok(new StatusResponse { StatusOk = true, StatusMessage = mensajeRetorno.mensaje });
            }
            else
            {
                return BadRequest(new StatusResponse { StatusOk = false, StatusMessage = mensajeRetorno.mensaje });
            }
        }

        //Quitar
        [HttpPost("/api/quitarProductos_Ingredientes")]
        public IActionResult Delete([FromBody] DTProductos_Ingredientes value)
        {
            MensajeRetorno mensajeRetorno = bl.quitarProductos_Ingredientes(value);
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
        [HttpGet("/api/listarIngredientesProducto{idProducto}")]
        public List<DTIngrediente> Get(int idProducto)
        {
            return bl.listar_IngredientesProducto(idProducto);
        }
    }
}
