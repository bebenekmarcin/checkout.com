using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Checkout.PaymentGatewayApi.Common;
using Checkout.PaymentGatewayApi.IntegrationTests.Testing;
using Checkout.PaymentGatewayApi.IntegrationTests.Testing.Extensions;
using Checkout.PaymentGatewayApi.Models;
using FluentAssertions;
using Xunit;

namespace Checkout.PaymentGatewayApi.IntegrationTests.Controllers
{
    public class PaymentApiShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly Fixture _fixture;

        public PaymentApiShould(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _fixture = new Fixture();
        }

        [Fact]
        public async Task GET_ReturnSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();
            var existingPaymentId = _factory.Database.Payment.Id;

            var response = await client.GetAsync($"api/payment/{existingPaymentId}");

            response.EnsureSuccessStatusCodeAndExpectedContent();
        }
        
        [Fact]
        public async Task GET_ReturnPayments()
        {
            var client = _factory.CreateClient();
            var existingPaymentId = _factory.Database.Payment.Id;

            var response = await client.GetAsync($"api/payment/{existingPaymentId}");

            response.EnsureSuccessStatusCodeAndExpectedContent();
            var payment = await response.Content.ReadAsJsonAsync<Payment>();
            payment.Should().BeEquivalentTo(_factory.Database.Payment);
        }

        [Fact]
        public async Task POST_ReturnSuccessAndCorrectContentType()
        {
            var client = _factory.CreateClient();
            var payment= _fixture.Create<Payment>();

            var response = await client.PostAsJsonAsync("api/payment", payment);

            response.EnsureSuccessStatusCodeAndExpectedContent();
        }
    }
}
