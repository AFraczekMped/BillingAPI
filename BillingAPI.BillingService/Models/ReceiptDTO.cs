using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.BillingService.Models
{
    public class ReceiptDTO
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public int OrderNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
