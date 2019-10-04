using System;

namespace Checkout.PaymentGatewayApi.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public bool IsSuccessful { get; set; }
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public int Cvv { get; set; }
    }
}
