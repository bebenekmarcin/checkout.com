using System.Threading.Tasks;
using AutoFixture;
using Checkout.PaymentGatewayApi.Database;
using Checkout.PaymentGatewayApi.Models;
using Checkout.PaymentGatewayApi.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Checkout.PaymentGatewayApi.UnitTests.Services
{
    public class PaymentServiceShould
    {
        private readonly PaymentService _paymentService;
        private readonly Mock<IAcquiringBankClient> _acquiringBankClientMock;
        private readonly Mock<IPaymentRepository> _paymentRepositoryMock;
        private readonly Fixture _fixture;

        public PaymentServiceShould()
        {
            _acquiringBankClientMock = new Mock<IAcquiringBankClient>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _paymentService = new PaymentService(_acquiringBankClientMock.Object, _paymentRepositoryMock.Object);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task ForwardPaymentRequestToAcquiringBank()
        {
            var payment = _fixture.Create<Payment>();

            await _paymentService.SendPaymentAsync(payment);

            _acquiringBankClientMock.Verify(b => b.SendPaymentAsync(payment), Times.Once);
        }

        [Fact]
        public async Task ReturnPaymentDetails()
        {
            var payment = _fixture.Create<Payment>();

            var paymentResponse = await _paymentService.SendPaymentAsync(payment);

            paymentResponse.Should().NotBeNull();
        }

        [Fact]
        public async Task StorePaymentDetailsInDatabase()
        {
            var payment = _fixture.Create<Payment>();

            await _paymentService.SendPaymentAsync(payment);

            _paymentRepositoryMock.Verify(r => r.SaveAsync(payment), Times.Once);
        }
    }
}
