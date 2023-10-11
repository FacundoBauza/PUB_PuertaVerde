using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class Productos_Ingredientes : Controller
    {

        private IBProductos_Ingredientes bl;
        public Productos_Ingredientes(IBProductos_Ingredientes _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/Productos_Ingredientes")]
        public ActionResult<DTProductos_Ingredientes> Post([FromBody] DTProductos_Ingredientes value)
        {
            MensajeRetorno x = bl.Productos_Ingredientes(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
    }
}
