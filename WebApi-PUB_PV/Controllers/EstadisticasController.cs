﻿using BusinessLayer.Interfaces;
using Domain.DT;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi_PUB_PV.Controllers
{
    public class EstadisticasController : Controller
    {
        private IB_Estadisticas bl;
        public EstadisticasController(IB_Estadisticas _bl)
        {
            bl = _bl;
        }

        //Listar todos los producto
        //[Authorize(Roles = "Admin")]
        [HttpPost("/api/todoslosproductos")]
        public List<DTProductoEstadistica> Gettodoslosproductos([FromBody] DTProductoEstadistica value)
        {
            /*if (!User.IsInRole("Admin"))
            {
                return null; // Devuelve 403 Forbidden si no tiene permisos
            }*/
            return bl.todoslosproductos(value);
        }
        //Listar los pdidos de un tipo
        [HttpPost("/api/productotipo")]
        public List<DTProductoEstadistica> Getproductotipo([FromBody] DTProductoEstadistica value)
        {
            return bl.productostipo(value);
        }
        //solo un pedido
        [HttpPost("/api/producto")]
        public DTProductoEstadistica Getproducto([FromBody] DTProductoEstadistica value)
        {
            return bl.producto(value);
        }

    }
}
