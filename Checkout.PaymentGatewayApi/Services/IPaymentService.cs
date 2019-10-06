using System;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Services
{
    public interface IPaymentService
    {
        /// <summary>Sends the payment to the acquiring bank and store the payment details.</summary>
        Task<Payment> SendPaymentAsync(Payment payment);

        /// <summary>Gets the payment details for given guid.</summary>
        Task<Payment> GetPaymentAsync(Guid guid);
    }
}