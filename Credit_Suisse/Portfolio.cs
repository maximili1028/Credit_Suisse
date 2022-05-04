using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Credit_Suisse
{
    public class Portfolio
    {
        public DateTime ReferenceDate { get; set; }
        public int NTrades { get; set; }
        public List<Trade> Trades { get; set; }

        public Portfolio() { Trades = new List<Trade>(); }

        public void CategorizeTrades()
        {
            foreach (Trade item in Trades)
            {
                if (GetEXPIRED(item))
                    item.Categoria = Category.EXPIRED;
                else if (GetHIGHRISK(item))
                    item.Categoria = Category.HIGHRISK;
                else if (GetMEDIUMRISK(item))
                    item.Categoria = Category.MEDIUMRISK;
                else
                    item.Categoria = "";
            }
        }

        private bool GetEXPIRED(Trade item)
        {
            TimeSpan diff = item.NextPaymentDate.Subtract(ReferenceDate);
            if (diff.Days < 30)
                return true;

            return false;
        }

        private static bool GetHIGHRISK(Trade item) 
        {
            if (item.ClientSector.Equals("Private") &&
                item.Value > 1000000)
                return true;

            return false; 
        }

        private bool GetMEDIUMRISK(Trade item)
        {
            if (item.ClientSector.Equals("Public") &&
                item.Value > 1000000)
                return true;

            return false;
        }
    }
}