using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Domain.Models;

namespace Checkout.PaymentGatewayApi.Domain.Services
{
    public interface IPaymentService
    {
        Task<Payment> SendPaymentAsync(Payment payment);
    }
}