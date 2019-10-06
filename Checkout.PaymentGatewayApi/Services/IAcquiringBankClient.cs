using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Services
{

    public interface IAcquiringBankClient
    {
        Task<BankResponse> SendPaymentAsync(Payment payment);
    }
}