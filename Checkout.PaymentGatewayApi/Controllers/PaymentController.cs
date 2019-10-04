using System;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Database;
using Checkout.PaymentGatewayApi.Models;
using Checkout.PaymentGatewayApi.Services;
using Microsoft.AspNetCore.Http;
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
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(
            ILogger<PaymentController> logger, 
            IPaymentService paymentService, 
            IPaymentRepository paymentRepository)
        {
            _logger = logger;
            _paymentService = paymentService;
            _paymentRepository = paymentRepository;
        }

        [HttpGet("{guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Payment>> GetPayment(Guid guid)
        {
            var payment = await _paymentRepository.GetAsync(guid);

            if (payment == null)
            {
                return NotFound();
            }

            return payment;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Payment>> SentPayment([FromBody] Payment payment)
        {
            var created = await _paymentService.SendPaymentAsync(payment);

            var response = new PaymentResponse
            {
                PaymentId = created.Id,
                IsSuccessful = created.IsSuccessful
            };

            return CreatedAtAction(nameof(GetPayment), new { guid = response.PaymentId }, payment);
        }
    }
}
