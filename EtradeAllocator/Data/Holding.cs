using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EtradeAllocator.Data
{
    public class Holding
    {
        Portfolio m_ParentPortfilo;

        public Holding(Portfolio parentPortfolio)
        {
            m_ParentPortfilo = parentPortfolio;
        }

        public AllocationType AllocType { get; set; }
        public string Symbol { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Value
        {
            get
            {
                return Quantity * Price;
            }
        }

        public decimal Alloc
        {
            get
            {
                return Value == decimal.Zero ? 
                    decimal.Zero : 
                    Value / m_ParentPortfilo.TotalValue;
            }
        }

        public decimal DesiredAlloc
        {
            get
            {
                return m_ParentPortfilo.DesiredPortfolio.GetAllocation(AllocType);                    
            }
        }

        public decimal AllocDiff
        {
            get
            {
                return Alloc - DesiredAlloc;
            }
        }
    }
}
