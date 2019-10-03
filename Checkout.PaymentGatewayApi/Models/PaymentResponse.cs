using System;

namespace Checkout.PaymentGatewayApi.Models
{
    public class PaymentResponse
    {
        public Guid PaymentId { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
