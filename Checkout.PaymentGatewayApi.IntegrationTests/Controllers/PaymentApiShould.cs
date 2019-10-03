using System.Collections.Generic;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Domain.Models;
using Checkout.PaymentGatewayApi.IntegrationTests.Extensions;
using Checkout.PaymentGatewayApi.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Checkout.PaymentGatewayApi.IntegrationTests.Controllers
{
    public class PaymentApiShould : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public PaymentApiShould(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GET_ReturnSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/payment");

            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Fact]
        public async Task GET_ReturnPayments()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("api/payment");

            var payments = await response.Content.ReadAsJsonAsync<IList<string>>();
            payments.Should().NotBeEmpty();
        }

        [Fact]
        public async Task POST_AcceptPaymentRequest()
        {
            var client = _factory.CreateClient();
            var paymentRequest = new PaymentRequest
            {
                CardNumber = "1234-1234-1234-1234",
                ExpiryMonth = 9,
                ExpiryYear = 2019,
                Currency = Currency.GPB,
                Amount = 1000,
                Cvv = 123
            };

            var response = await client.PostAsJsonAsync("api/payment", paymentRequest);

            response.EnsureSuccessStatusCode();
        }
    }
}
