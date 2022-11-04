using NUnit.Framework;
using BillingAPI.BillingService.Requests.OrdersActions;
using AutoFixture;
using BillingAPI.BillingService.Models;
using System.Threading.Tasks;
using System.Threading;
using BillingAPI.BillingService.Repositories;
using BillingAPI.BillingService.FileGenerators;

namespace BillingAPI.Tests
{
    public class BillingAPITests
    {

        private ProcessOrderCommand _command;
        private ProcessOrderHandler _handler;
        private OrderRepository _orderRepository;
        private IFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new Fixture();
            _command = new ProcessOrderCommand();
            _orderRepository = new OrderRepository();
            _handler = new ProcessOrderHandler(_orderRepository);
            SetUpCommand();
        }
        [Test]
        public async Task ProcessOrderHandler_WhenCalled_ReturnString()
        {
            var result = await _handler.Handle(_command, CancellationToken.None);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<string>());
        }

        private void SetUpCommand()
        {
            var orderDto = _fixture.Build<OrderDTO>().With(x => x.PaymentGateway, 1).Create();
            _command.Order = orderDto;
        }
        [Test]
        public void GenerateReceipt_WhenCalled_ReturnarrayArrayByte()
        {
            var generator = new PdfReceiptGenerator();
            ReceiptDTO receipt = new ReceiptDTO()
            {
                CompanyName = "XYZ Inc.",
                CompanyAddress = "6818 Eget St., Tacoma AL 92508",
                Amount = 222.33m,
                OrderNumber = 1
            };
            var result = generator.GenerateReceipt(receipt);
            Assert.IsNotNull(result);
            Assert.That(result, Is.TypeOf<byte[]>());
        }
    }
}
