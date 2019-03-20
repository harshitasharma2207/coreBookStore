using System;
using System.Collections.Generic;

namespace OnlineBookStoreUser.Models
{
    public partial class OrderBook
    {
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public Books Book { get; set; }
        public Order Order { get; set; }
    }
}
