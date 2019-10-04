using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Services
{

    public interface IAcquiringBankClient
    {
        Task<Payment> SendPaymentAsync(Payment payment);
    }
}