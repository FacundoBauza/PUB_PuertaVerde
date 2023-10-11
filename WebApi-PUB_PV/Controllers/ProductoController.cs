using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Http;
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
        public ActionResult<DTProducto> Post([FromBody] DTProducto value)
        {
            MensajeRetorno x = bl.agregar_Producto(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/listarProductos")]
        public List<DTProducto> Get()
        {
            return bl.listar_Productos();
        }

        //Eliminar
        [HttpDelete("/api/bajaProducto/{id:int}")]
        public ActionResult<bool> BajaProducto(int id)
        {
            MensajeRetorno x = bl.baja_Producto(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Modificar
        [HttpPut("/api/modificarProducto")]
        public ActionResult<DTProducto> Put([FromBody] DTProducto Modificar)
        {
            MensajeRetorno x = bl.Modificar_Producto(Modificar);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
    }
}
