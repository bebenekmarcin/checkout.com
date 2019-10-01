namespace Checkout.PaymentGatewayApi.Models
{
    public class PaymentRequest
    {
        public string CardNumber { get; set; }
        public string ExpiryMonth { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Cvv { get; set; }
    }
}
