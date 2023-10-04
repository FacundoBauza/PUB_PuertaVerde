﻿using Domain.DT;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DataAccesLayer.Models
{
    [Table(name: "Pedido")]
    public class Pedidos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int id_Pedido { get; set; }
        public float valorPedido { get; set; }
        public bool estadoProceso { get; set; }
        public DateTime hora_ingreso { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string numero_movil { get; set; }
        public bool pago { get; set; }
        public string observaciones { get; set; }

        [ForeignKey("User")]
        public string username { get; set; }

        [ForeignKey("Cliente_Preferencial")]
        public int id_Cli_Preferencial { get; set; }

        [ForeignKey("Mesa")]
        public int id_Mesa { get; set; }


        public static Pedidos SetPedido(DTPedido x)
        {
            Pedidos aux = new Pedidos();

            aux.id_Pedido = x.id_Pedido;
            aux.valorPedido = x.valorPedido;
            aux.estadoProceso = true;
            aux.hora_ingreso = x.hora_ingreso;
            aux.fecha_ingreso = x.fecha_ingreso;
            aux.numero_movil = x.numero_movil;
            aux.username = x.username;
            aux.id_Cli_Preferencial = x.id_Cli_Preferencial;
            aux.id_Mesa = x.id_Mesa;
            aux.observaciones = x.observaciones;

            //aux.registro_Activo = true;

            return aux;
        }

        public Pedidos GetPedido()
        {
            Pedidos aux = new Pedidos();

            aux.id_Pedido = id_Pedido;
            aux.valorPedido = valorPedido;
            aux.estadoProceso = estadoProceso;
            aux.hora_ingreso = hora_ingreso;
            aux.fecha_ingreso = fecha_ingreso;
            aux.numero_movil = numero_movil;
            aux.username = username;
            aux.id_Cli_Preferencial = id_Cli_Preferencial;
            aux.id_Mesa = id_Mesa;
            aux.observaciones = observaciones;

            //aux.registro_Activo = registro_Activo;

            return aux;
        }
    }
}
