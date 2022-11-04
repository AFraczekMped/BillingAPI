using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.BillingService.Models
{
    public class OrderDTO
    {
        public int OrderNumber { get; set; }
        public Guid UserId { get; set; }
        public decimal PayableAmount { get; set; }
        public int PaymentGateway { get; set; }
        public string Description { get; set; }
    }
}
