using System.Threading.Tasks;
using AutoFixture;
using Checkout.PaymentGatewayApi.Database;
using Checkout.PaymentGatewayApi.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Checkout.PaymentGatewayApi.IntegrationTests
{
    public class PaymentRepositoryShould
    {
        private readonly PaymentDbContext _paymentContext;
        private readonly PaymentRepository _paymentRepository;
        private readonly Fixture _fixture;
        public PaymentRepositoryShould()
        {
            var options = new DbContextOptionsBuilder<PaymentDbContext>()
                .UseInMemoryDatabase("TestPaymentDatabase")
                .Options;
            _paymentContext = new PaymentDbContext(options);

            _paymentRepository = new PaymentRepository(_paymentContext);
            _fixture = new Fixture();
        }

        [Fact]
        public async Task SavePayment()
        {
            var payment = _fixture.Create<Payment>();

            await _paymentRepository.SaveAsync(payment);

            var paymentAInDb =
                await _paymentContext.Payments.SingleOrDefaultAsync(p => p.Id == payment.Id);
            paymentAInDb.Should().NotBeNull();
        }

        [Fact]
        public async Task ReturnPaymentForGivenId()
        {
            var payment = _fixture.Create<Payment>();
            await _paymentContext.Payments.AddAsync(payment);
            await _paymentContext.SaveChangesAsync();

            var paymentAInDb = await _paymentRepository.GetAsync(payment.Id);

            paymentAInDb.Should().NotBeNull();
        }
    }
}
