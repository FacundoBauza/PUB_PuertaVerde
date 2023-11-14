using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class ProductoController : Controller
    {

        private IB_Producto bl;
        public ProductoController(IB_Producto _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarProducto")]
        public IActionResult Post([FromBody] DTProducto value)
        {
            MensajeRetorno mensajeRetorno = bl.agregar_Producto(value);
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
        [HttpGet("/api/listarProductos")]
        public List<DTProducto> Get()
        {
            return bl.listar_Productos();
        }

        //Listar
        [HttpGet("/api/listarProductosPorTipo{tipo}")]
        public List<DTProducto> GetProductosPorTipo(Domain.Enums.Categoria tipo)
        {
            return bl.listar_ProductosPorTipo(tipo);
        }

        //Eliminar
        [HttpDelete("/api/bajaProducto/{id:int}")]
        public IActionResult BajaProducto(int id)
        {
            MensajeRetorno mensajeRetorno = bl.baja_Producto(id);
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
        [HttpPut("/api/modificarProducto")]
        public IActionResult Put([FromBody] DTProducto Modificar)
        {
            MensajeRetorno mensajeRetorno = bl.Modificar_Producto(Modificar);
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
