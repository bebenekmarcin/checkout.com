using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Domain.Models;

namespace Checkout.PaymentGatewayApi.Domain.Services
{
    public class PaymentRepository : IPaymentRepository
    {
        public Task AddPaymentAsync(Payment payment)
        {
            throw new NotImplementedException();
        }
    }
}
