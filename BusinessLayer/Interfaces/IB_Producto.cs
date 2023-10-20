﻿using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_Producto
    {
        MensajeRetorno agregar_Producto(DTProducto value);
        MensajeRetorno baja_Producto(int id);
        List<DTProducto> listar_Productos();
        List<DTProducto> listar_ProductosPorTipo(Domain.Enums.Categoria tipo);
        MensajeRetorno Modificar_Producto(DTProducto modificar);
    }
}
