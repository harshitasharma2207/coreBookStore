using OnlineBookStoreUser.Models;
using System;
using System.Collections.Generic;

namespace coreBookStore.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public float OrderAmount { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public List<OrderBook> OrderBook { get; set; }
    }
}
