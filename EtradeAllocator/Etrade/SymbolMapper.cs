using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EtradeAllocator.Data;

namespace EtradeAllocator.Etrade
{
    public class SymbolMapper
    {
        private Dictionary<string, AllocationType> m_Mappings = new Dictionary<string, AllocationType>();

        public SymbolMapper()
        {
            // TODO: Load from somewhere.
            m_Mappings.Add("HABDX", AllocationType.Bonds);
            m_Mappings.Add("FNMIX", AllocationType.BondsEmergingMarkets);
            m_Mappings.Add("VUG", AllocationType.EquityLargeCapGrowth);
            m_Mappings.Add("DIA", AllocationType.EquityLargeCapValue);
            m_Mappings.Add("VBK", AllocationType.EquitySmallMedCapGrowth);
            m_Mappings.Add("VOE", AllocationType.EquitySmallMedCapValue);
            m_Mappings.Add("IJH", AllocationType.EquityForeignBlend);
            m_Mappings.Add("ADRE", AllocationType.EquityEmergingMarketsBlend);
        }

        public AllocationType GetAllocationType(string symbol)
        {
            if(!m_Mappings.ContainsKey(symbol))
            {
                throw new Exception(string.Format("Symbol mapping not found for '{0}'.", symbol));
            }

            return m_Mappings[symbol];
        }
    }
}
