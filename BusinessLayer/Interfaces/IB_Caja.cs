using DataAccesLayer.Models;
using Domain.DT;
using Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IB_Caja
    {
        //Agregar
        MensajeRetorno Set_Cajas(Domain.DT.DTCaja dtc);

        //Listar
        List<DTCaja> GetCajas();


        //Modificar
        public MensajeRetorno Modificar_Cajas(Domain.DT.DTCaja dtc);

        //Baja
        MensajeRetorno Baja_Cajas(int id);
    }
}
