using System;
using Checkout.PaymentGatewayApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.AcquiringBankSimulator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase
    {
        private readonly ILogger<BankController> _logger;

        public BankController(ILogger<BankController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public BankResponse GetBankResponse()
        {
            return new BankResponse
            {
                IsSuccessful = true,
                PaymentId = Guid.NewGuid()
            };
        }

        [HttpPost]
        public BankResponse AcceptPayment(Payment payment)
        {
            return new BankResponse
            {
                IsSuccessful = true,
                PaymentId = Guid.NewGuid()
            };
        }
    }
}
