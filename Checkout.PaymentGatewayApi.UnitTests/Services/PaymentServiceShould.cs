using System;
using System.Threading.Tasks;
using AutoFixture;
using Checkout.PaymentGatewayApi.Database;
using Checkout.PaymentGatewayApi.Logging;
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
        private readonly Mock<ILoggerAdapter<PaymentService>> _loggerMock;
        private readonly Fixture _fixture;
        private readonly BankResponse _bankResponse;

        public PaymentServiceShould()
        {
            _acquiringBankClientMock = new Mock<IAcquiringBankClient>();
            _paymentRepositoryMock = new Mock<IPaymentRepository>();
            _loggerMock = new Mock<ILoggerAdapter<PaymentService>>();
            _paymentService = new PaymentService(_loggerMock.Object, _acquiringBankClientMock.Object, _paymentRepositoryMock.Object);
            _fixture = new Fixture();

            _bankResponse = _fixture.Create<BankResponse>();
            _acquiringBankClientMock.Setup(b => b.SendPaymentAsync(It.IsAny<Payment>()))
                .Returns(Task.FromResult(_bankResponse));
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

            paymentResponse.Should().BeEquivalentTo(payment);
        }

        [Fact]
        public async Task UpdatePaymentIdAndStatusByResponseFromBank()
        {
            var payment = _fixture.Build<Payment>()
                .With(p=>p.Id, Guid.Empty)
                .With(p=>p.IsSuccessful, false)
                .Create();

            var paymentResponse = await _paymentService.SendPaymentAsync(payment);

            paymentResponse.Id.Should().Be(_bankResponse.PaymentId);
            paymentResponse.IsSuccessful.Should().Be(_bankResponse.IsSuccessful);

        }

        [Fact]
        public async Task StorePaymentDetailsInDatabase()
        {
            var payment = _fixture.Create<Payment>();

            await _paymentService.SendPaymentAsync(payment);

            _paymentRepositoryMock.Verify(r => r.SaveAsync(payment), Times.Once);
        }

        [Fact]
        public async Task LogError_WhenExceptionOccur()
        {
            var payment = _fixture.Create<Payment>();
            _paymentRepositoryMock.Setup(r => r.SaveAsync(It.IsAny<Payment>())).Throws(new Exception());

            try
            {
                await _paymentService.SendPaymentAsync(payment);
            }
            catch (Exception)
            {
                // ignored
            }

            _loggerMock.Verify(x => x.LogError(It.IsAny<Exception>()), Times.Once);
        }


        [Fact]
        public async Task GetPaymentFromRepository()
        {
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111111111111111").Create();
            _paymentRepositoryMock.Setup(r => r.GetAsync(payment.Id)).Returns(Task.FromResult(payment));

            await _paymentService.GetPaymentAsync(payment.Id);

            _paymentRepositoryMock.Verify(r => r.GetAsync(payment.Id), Times.Once);
        }

        [Fact]
        public async Task MaskCardNumber()
        {
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111111111111111").Create();
            _paymentRepositoryMock.Setup(r => r.GetAsync(payment.Id)).Returns(Task.FromResult(payment));

            var paymentFromDb = await _paymentService.GetPaymentAsync(payment.Id);

            paymentFromDb.CardNumber.Should().Be("************1111");
        }
    }
}
