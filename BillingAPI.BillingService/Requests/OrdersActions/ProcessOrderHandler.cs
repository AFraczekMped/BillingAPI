using BillingAPI.BillingService.Enums;
using BillingAPI.BillingService.Interfaces;
using BillingAPI.BillingService.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using BillingAPI.BillingService.FileGenerators;

namespace BillingAPI.BillingService.Requests.OrdersActions
{
    public class ProcessOrderHandler : IRequestHandler<ProcessOrderCommand, string>
    {
        private readonly IOrderRepository _orderRepository;
        public ProcessOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }
        public async Task<string> Handle(ProcessOrderCommand request, CancellationToken cancellationToken)
        {
            OrderDTO order = new OrderDTO()
            {
                OrderNumber = request.Order.OrderNumber,
                UserId = request.Order.UserId,
                PayableAmount = request.Order.PayableAmount,
                PaymentGateway = request.Order.PaymentGateway,
                Description = request.Order.Description,
            };

            try
            {
                await _orderRepository.AddOrder(order);
                //saving changes to database
                bool isPaymentSuccessed = CreatePayment(order);

                if (isPaymentSuccessed)
                {
                    return CreateReceipt(order);
                }
                else{
                    throw new Exception("Error creating payment");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        private bool CreatePayment(OrderDTO order)
        {
            bool response = false;
            switch (order.PaymentGateway)
            {
                case (int)PaymentSystem.System1:
                    response = CreateSystem1Payment(order);
                    break;
                case (int)PaymentSystem.System2:
                    response = CreateSystem2Payment(order);
                    break;
                default:
                    break;
            }
            return response;
        }

        private string CreateReceipt(OrderDTO order)
        {
            ReceiptDTO receipt = new ReceiptDTO()
            {
                CompanyName = "XYZ Inc.",
                CompanyAddress = "6818 Eget St., Tacoma AL 92508",
                Amount = order.PayableAmount,
                OrderNumber = order.OrderNumber
            };
            var pdfGenerator = new PdfReceiptGenerator();
            byte[] pdfReceipt = pdfGenerator.GenerateReceipt(receipt);
            return Convert.ToBase64String(pdfReceipt);
        }

        private bool CreateSystem1Payment(OrderDTO order)
        {
            try
            {
                //mapping the order to the payment system model and sending request to System1 url address
                return true;
            }
            catch
            {
                return false;
            }
        }
        private bool CreateSystem2Payment(OrderDTO order)
        {
            try
            {
                //mapping the order to the payment system model and sending request to System2 url address
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
