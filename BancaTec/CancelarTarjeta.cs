using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BancaTec
{
    public partial class CancelarTarjeta
    {
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int NumTarjeta { get; set; }
        public int Id { get; set; }
        [XmlIgnore]
        public char Estado { get; set; }

        [XmlIgnore]
        public virtual Tarjeta NumTarjetaNavigation { get; set; }
    }
}
