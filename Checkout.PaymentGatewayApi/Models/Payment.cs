using System;
using System.ComponentModel.DataAnnotations;

namespace Checkout.PaymentGatewayApi.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public bool IsSuccessful { get; set; }
        
        [Required, CreditCard]
        public string CardNumber { get; set; }

        [Required, Range(2019, int.MaxValue, ErrorMessage = "The ExpiryMonth should be between 1 and 12.")]
        public int ExpiryMonth { get; set; }

        [Required, Range(2019, int.MaxValue, ErrorMessage = "The ExpiryYear should be greater than 2019.")]
        public int ExpiryYear { get; set; }

        [Required, Range(typeof(decimal), "0.01", "79228162514264337593543950335", ErrorMessage = "The Amount should be greater than £0.01.")]
        public decimal Amount { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required, Range(100, 999, ErrorMessage = "The Cvv should have 3 digits length")]
        public int Cvv { get; set; }
    }
}
