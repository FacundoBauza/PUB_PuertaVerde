﻿using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_ClientePreferencial
    {
        //Agregar
        MensajeRetorno agregar_ClientePreferencial(DTCliente_Preferencial dtCP);
    }
}
