using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Domain.Models;
using Checkout.PaymentGatewayApi.Domain.Services;
using Checkout.PaymentGatewayApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Checkout.PaymentGatewayApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ILogger<PaymentController> _logger;
        private readonly IPaymentService _paymentService;

        public PaymentController(ILogger<PaymentController> logger, IPaymentService paymentService)
        {
            _logger = logger;
            _paymentService = paymentService;
        }

        // GET: api/Payment
        [HttpGet("{guid}")]
        public IEnumerable<string> GetPayment(Guid guid)
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<ActionResult<PaymentResponse>> Post([FromBody] Payment payment)
        {
            var created = await _paymentService.SendPaymentAsync(payment);

            var response = new PaymentResponse
            {
                PaymentId = created.PaymentId,
                IsSuccessful = created.IsSuccessful
            };

            return CreatedAtAction(nameof(GetPayment), new { id = response.PaymentId }, payment);
        }
    }
}
