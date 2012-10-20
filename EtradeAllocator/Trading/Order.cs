using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EtradeAllocator.Trading
{
    public class Order
    {
        public OrderType Type { get; private set; }
        public string Symbol { get; private set; }
        public decimal Price { get; private set; }
        public decimal Quantity { get; private set; }

        public decimal Value
        {
            get { return Price * Quantity; }
        }

        public Order(OrderType type, string symbol, decimal limitPrice, decimal quantity)
        {
            Type = type;
            Symbol = symbol;
            Price = limitPrice;
            Quantity = quantity;
        }

    }
}
