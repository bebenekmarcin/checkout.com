using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Services
{

    public interface IAcquiringBankClient
    {
        /// <summary>Sends the payment to the Acquiring Bank.</summary>
        Task<BankResponse> SendPaymentAsync(Payment payment);
    }
}