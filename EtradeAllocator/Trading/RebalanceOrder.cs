using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EtradeAllocator.Data;

namespace EtradeAllocator.Trading
{
    public class RebalanceOrder
    {
        private readonly Portfolio m_ParentPortfolio;

        public readonly List<Order> Orders = new List<Order>();

        public decimal LeftOverCapital { get; private set; }

        public RebalanceOrder(Portfolio portfolio)
        {
            m_ParentPortfolio = portfolio;

            BuildOrders();
        }

        private void BuildOrders()
        {
            LeftOverCapital = 0;

            foreach (Holding holding in m_ParentPortfolio.Holdings)
            {
                decimal priceDiff = holding.AllocDiff * m_ParentPortfolio.TotalValue;

                OrderType orderType = priceDiff > 0 ? OrderType.Sell : OrderType.Buy;

                decimal rawQuantity = Math.Abs(priceDiff)/holding.Price;
                decimal quantity = Math.Floor(rawQuantity);

                Orders.Add(new Order(orderType, holding.Symbol, holding.Price, quantity));

                LeftOverCapital += (rawQuantity-quantity)*holding.Price;
            }
        }
    }
}
