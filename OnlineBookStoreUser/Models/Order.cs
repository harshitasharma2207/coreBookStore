using System;
using System.Collections.Generic;

namespace OnlineBookStoreUser.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderBook = new HashSet<OrderBook>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public float OrderAmount { get; set; }
        public int CustomerId { get; set; }

        public Customers Customer { get; set; }
        public ICollection<OrderBook> OrderBook { get; set; }
    }
}
