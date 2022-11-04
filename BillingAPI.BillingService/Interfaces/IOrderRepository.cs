using BillingAPI.BillingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.BillingService.Interfaces
{
    public interface IOrderRepository
    {
        public Task AddOrder(OrderDTO order);
    }
}
