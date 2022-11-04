using BillingAPI.BillingService.Interfaces;
using BillingAPI.BillingService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillingAPI.BillingService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public async Task AddOrder(OrderDTO order)
        {
                //adding order to database
        }
    }
}
