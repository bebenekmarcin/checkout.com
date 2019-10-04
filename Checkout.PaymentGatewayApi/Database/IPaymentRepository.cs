using System;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Database
{
    public interface IPaymentRepository
    {
        Task<Payment> GetAsync(Guid paymentId);
        Task SaveAsync(Payment payment);
    }
}