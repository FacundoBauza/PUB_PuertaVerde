using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
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
        public ActionResult<DTCategoria> Post([FromBody] DTCaja value)
        {
            MensajeRetorno x = bl.Set_Cajas(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //modificar
        [HttpPut("/api/listarCajas")]
        public ActionResult<DTCategoria> Put([FromBody] DTCaja Modificar)
        {
            MensajeRetorno x = bl.Modificar_Cajas(Modificar);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/listarCaja")]
        public List<DTCaja> Get()
        {
            return bl.GetCajas();
        }

        //Listar activa
        [HttpGet("/api/listarCajaavtiva")]
        public List<DTCaja> Getactivas()
        {
            return bl.GetCajasactivas();
        }
        //Eliminar
        [HttpDelete("/api/bajaCaja/{id:int}")]
        public ActionResult<bool> BajaCaja(int id)
        {
            MensajeRetorno x = bl.Baja_Cajas(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

    }
}
