using BillingAPI.BillingService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BillingAPI.BillingService.Requests.OrdersActions
{
    public class ProcessOrderCommand : IRequest<string>
    {
        public OrderDTO Order { get; set; }
    }
}
