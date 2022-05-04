using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credit_Suisse
{
    public class Trade : ITrade
    {
        public double Value { get; set; }
        public string ClientSector { get; set; }
        public DateTime NextPaymentDate { get; set; }


        public enum TradeContent { Value = 0, ClientSector  = 1, NextPaymentDate  = 2}
        public string Categoria { get; set; }
    }
}
