using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Services
{
    public class AcquiringBankClient : IAcquiringBankClient
    {
        public async Task<Payment> SendPaymentAsync(Payment payment)
        {
            return new Payment();
        }
    }
}
