using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Domain.Models;

namespace Checkout.PaymentGatewayApi.Domain.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAcquiringBankClient _acquiringBankClient;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IAcquiringBankClient acquiringBankClient, IPaymentRepository paymentRepository)
        {
            _acquiringBankClient = acquiringBankClient;
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> SendPaymentAsync(Payment payment)
        {
            await _acquiringBankClient.SendPaymentAsync(payment);
            await _paymentRepository.AddPaymentAsync(payment);

            return payment;
        }
    }
}