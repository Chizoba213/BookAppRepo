using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Entities
{
    public class OrderBook
    {
        public int OrderBookID { get; set; }
        //public int OrderID { get; set; }
        public int BookID { get; set; }
        //public string BookName { get; set; }
        public string CustomerName { get; set; }
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; }
        //public string AuthorName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        
    }
}
