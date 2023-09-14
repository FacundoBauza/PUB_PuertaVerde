﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entidades
{
    public class Producto
    {
        public int id_Producto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public float precio { get; set; }

        public Producto()
        {
        }

        public Producto(int id_Producto, string nombre, string descripcion, float precio)
        {
            this.id_Producto = id_Producto;
            this.nombre = nombre;
            this.descripcion = descripcion;
            this.precio = precio;
        }
    }
}
