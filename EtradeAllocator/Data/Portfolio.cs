using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EtradeAllocator.Data
{
    public class Portfolio
    {
        private decimal m_TotalValue = new Decimal(0.0);

        public readonly List<Holding> Holdings = new List<Holding>();

        public readonly DesiredPortfolio DesiredPortfolio;

        public Portfolio(DesiredPortfolio desiredPorfolio)
        {
            DesiredPortfolio = desiredPorfolio;
        }

        public void AddHolding(AllocationType allocType, string symbol, decimal price, decimal quanity)
        {
            Holding holding = new Holding(this)
                    {
                        AllocType = allocType,
                        Symbol = symbol,
                        Price = price,
                        Quantity = quanity,
                    };

            m_TotalValue += holding.Value;

            Holdings.Add(holding);
        }

        public decimal TotalValue 
        {
            get
            {
                return m_TotalValue;
            }
        }

    }
}
