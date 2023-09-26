﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DT
{
    public class DTMesa
    {
        public int id_Mesa { get; set; }
        public bool enUso { get; set; }

        public DTMesa()
        {
        }

        public DTMesa(int id_Mesa, bool enUso)
        {
            this.id_Mesa = id_Mesa;
            this.enUso = enUso;
        }
    }
}
