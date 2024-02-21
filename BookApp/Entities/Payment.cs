using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public int OrderID { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
