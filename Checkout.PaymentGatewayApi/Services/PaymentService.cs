using System;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Database;
using Checkout.PaymentGatewayApi.Logging;
using Checkout.PaymentGatewayApi.Models;


namespace Checkout.PaymentGatewayApi.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ILoggerAdapter<PaymentService> _logger;
        private readonly IAcquiringBankClient _acquiringBankClient;
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(
            ILoggerAdapter<PaymentService> logger,
            IAcquiringBankClient acquiringBankClient,
            IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _acquiringBankClient = acquiringBankClient;
            _paymentRepository = paymentRepository;
        }

        public async Task<Payment> SendPaymentAsync(Payment payment)
        {
            try
            {
                await _acquiringBankClient.SendPaymentAsync(payment);
                await _paymentRepository.SaveAsync(payment);

                return payment;
            }
            catch (Exception e)
            {
                _logger.LogError(e);
                throw;
            }
        }

        public async Task<Payment> GetPaymentAsync(Guid guid)
        {
            var payment = await _paymentRepository.GetAsync(guid);

            payment.CardNumber = payment.CardNumber.Substring(payment.CardNumber.Length - 4).PadLeft(payment.CardNumber.Length, '*');

            return payment;
        }
    }
}