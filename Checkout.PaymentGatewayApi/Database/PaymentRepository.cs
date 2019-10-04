using System;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Database
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _paymentContext;

        public PaymentRepository(PaymentDbContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Payment> GetAsync(Guid paymentId)
        {
            return await _paymentContext.Payments.FindAsync(paymentId);
        }

        public async Task SaveAsync(Payment payment)
        {
            await _paymentContext.Payments.AddAsync(payment);

            await _paymentContext.SaveChangesAsync();
        }
    }
}
