﻿using System;
using System.Collections.Generic;

namespace BancaTec
{
    public partial class Compra
    {
        public int NumTarjeta { get; set; }
        public string Comercio { get; set; }
        public decimal Monto { get; set; }
        public string Moneda { get; set; }
        public int Id { get; set; }

        public virtual Tarjeta NumTarjetaNavigation { get; set; }
    }
}
