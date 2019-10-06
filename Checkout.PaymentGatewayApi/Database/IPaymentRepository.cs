using System;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Database
{
    public interface IPaymentRepository
    {
        /// <summary>Gets the payment for given id.</summary>
        Task<Payment> GetAsync(Guid paymentId);


        /// <summary>Saves the payment in the database.</summary>
        Task SaveAsync(Payment payment);
    }
}