﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Db.Dto
{
    public class InscriptionDatesDto
    {
        public DateTime fechaInicio;
        public DateTime fechaFin;
        public string devolucion;



        public override string ToString()
        {

            return this.fechaInicio.ToShortDateString() + " - " + this.fechaFin.ToShortDateString()+ " DEVOLUCION DEL : " + devolucion;
        }
    }
}
