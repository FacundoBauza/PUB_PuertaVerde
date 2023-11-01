using BusinessLayer.Interfaces;
using Domain.DT;
using Domain.Entidades;
using Microsoft.AspNetCore.Mvc;
using WebApi_PUB_PV.Models;

namespace WebApi_PUB_PV.Controllers
{
    public class MesaController : Controller
    {

        private readonly IB_Mesa bl;
        public MesaController(IB_Mesa _bl)
        {
            bl = _bl;
        }

        //Agregar
        [HttpPost("/api/agregarMesa")]
        public ActionResult<DTMesa> Post([FromBody] DTMesa value)
        {
            MensajeRetorno x = bl.Agregar_Mesa(value);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Listar
        [HttpGet("/api/listarMesas")]
        public List<DTMesa> Get()
        {
            return bl.Listar_Mesas();
        }

        //Eliminar
        [HttpDelete("/api/bajaMesa/{id:int}")]
        public ActionResult<bool> BajaMesa(int id)
        {
            MensajeRetorno x = bl.Baja_Mesa(id);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }

        //Modificar
        [HttpPut("/api/modificarMesa")]
        public ActionResult<DTMesa> Put([FromBody] DTMesa Modificar)
        {
            MensajeRetorno x = bl.Modificar_Mesa(Modificar);
            return Ok(new StatusResponse { StatusOk = x.status, StatusMessage = x.mensaje });
        }
        //Cerar cuenta de la mesa
        [HttpPut("/api/cerarCuentaMesa")]
        public ActionResult<byte[]> CerarMesa([FromBody] DTMesa Modificar)
        {
            return bl.CerarMesa(Modificar);
        }
    }
}
