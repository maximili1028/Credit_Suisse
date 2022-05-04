using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credit_Suisse
{
    internal interface ITrade
    {
        double Value { get; set; }
        string ClientSector { get; set; }
        DateTime NextPaymentDate { get; set; }
    }
}
