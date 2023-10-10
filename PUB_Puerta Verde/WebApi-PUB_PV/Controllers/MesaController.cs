using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class MesaController : Controller
    {

        private IB_Mesa bl;
        public MesaController(IB_Mesa _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarMesa")]
        public ActionResult<DTMesa> Post([FromBody] DTMesa value)
        {
            MensajeRetorno x = bl.agregar_Mesa(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/listarMesas")]
        public List<DTMesa> Get()
        {
            return bl.listar_Mesas();
        }

        //Eliminar
        [HttpDelete("/api/bajaMesa/{id:int}")]
        public ActionResult<bool> BajaMesa(int id)
        {
            MensajeRetorno x = bl.baja_Mesa(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Modificar
        [HttpPut("/api/modificarMesa")]
        public ActionResult<DTMesa> Put([FromBody] DTMesa Modificar)
        {
            MensajeRetorno x = bl.Modificar_Mesa(Modificar);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
        //Modificar
        [HttpPut("/api/cerarCuentaMesa")]
        public ActionResult<byte[]> cerarMesa([FromBody] DTMesa Modificar)
        {
            return bl.cerarMesa(Modificar);
        }
    }
}
