using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EtradeAllocator.Data
{
    public class DesiredPortfolio
    {
        private readonly Dictionary<AllocationType, decimal> m_Allocations = new Dictionary<AllocationType, decimal>();

        public DesiredPortfolio()
        {
            // Init all types to 0.
            foreach (AllocationType type in Enum.GetValues(typeof(AllocationType)))
            {
                m_Allocations.Add(type, new Decimal(0.0));
            }

            // Set the Allocs
            // TODO: Load from somewhere
            m_Allocations[AllocationType.Bonds] = new Decimal(0.13);
            m_Allocations[AllocationType.BondsEmergingMarkets] = new Decimal(0.07);
            m_Allocations[AllocationType.EquityLargeCapGrowth] = new Decimal(0.2);
            m_Allocations[AllocationType.EquityLargeCapValue] = new Decimal(0.2);
            m_Allocations[AllocationType.EquitySmallMedCapGrowth] = new Decimal(0.1);
            m_Allocations[AllocationType.EquitySmallMedCapValue] = new Decimal(0.1);
            m_Allocations[AllocationType.EquityForeignBlend] = new Decimal(0.15);
            m_Allocations[AllocationType.EquityEmergingMarketsBlend] = new Decimal(0.05);
            m_Allocations[AllocationType.MoneyMarket] = new Decimal(0.0);
            m_Allocations[AllocationType.Cash] = new Decimal(0);

            // Validate
            decimal sum = 0;
            foreach (AllocationType type in Enum.GetValues(typeof(AllocationType)))
            {
                sum += m_Allocations[type];
            }

            if (sum != new decimal(1.0))
            {
                throw new Exception("Allocations do not sum to 100%");
            }
        }

        public decimal GetAllocation(AllocationType allocType)
        {
            if (!m_Allocations.ContainsKey(allocType))
            {
                throw new ArgumentException(string.Format("AllocationType '{0}' is not defined.",allocType));
            }

            return m_Allocations[allocType];
        }
    }
}
