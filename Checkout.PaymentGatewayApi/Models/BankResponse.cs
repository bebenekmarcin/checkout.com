using System;

namespace Checkout.PaymentGatewayApi.Models
{
    public class BankResponse
    {
        public Guid PaymentId { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
