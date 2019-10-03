using System.Threading.Tasks;
using AutoFixture;
using Checkout.PaymentGatewayApi.Controllers;
using Checkout.PaymentGatewayApi.Domain.Models;
using Checkout.PaymentGatewayApi.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Checkout.PaymentGatewayApi.UnitTests.Controllers
{
    public class PaymentControllerShould
    {
        private readonly Mock<ILogger<PaymentController>> _loggerMock;
        private readonly Mock<IPaymentService> _paymentServiceMock;
        private readonly PaymentController _paymentController;
        private readonly Fixture _fixture;

        public PaymentControllerShould()
        {
            _fixture = new Fixture();
            _loggerMock = new Mock<ILogger<PaymentController>>();
            _paymentServiceMock = new Mock<IPaymentService>();
            _paymentController = new PaymentController(_loggerMock.Object, _paymentServiceMock.Object);
        }

        [Fact]
        public async Task ReturnSuccess_WhenPaymentIsProcessedSuccessfully()
        {
            var payment = _fixture.Create<Payment>();

            var response = await _paymentController.Post(payment);

        }

    }
}
