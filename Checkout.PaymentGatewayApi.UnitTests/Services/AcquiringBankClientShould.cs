using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Checkout.PaymentGatewayApi.Configuration;
using Checkout.PaymentGatewayApi.Models;
using Checkout.PaymentGatewayApi.Services;
using Checkout.PaymentGatewayApi.Testing;
using FluentAssertions;
using Xunit;


namespace Checkout.PaymentGatewayApi.UnitTests.Services
{
    public class AcquiringBankClientShould
    {
        private readonly HttpMessageHandlerProxy _httpMessageHandlerProxy;
        private readonly AcquiringBankClient _acquiringBankClient;
        private readonly Fixture _fixture;

        public AcquiringBankClientShould()
        {
            _fixture = new Fixture();
            _httpMessageHandlerProxy = new HttpMessageHandlerProxy();
            HttpClient httpClient = new HttpClient(_httpMessageHandlerProxy);
            var config = new AcquiringBankConfig
            {
                BaseAddress = "http://tempuri.org/api/"
            };

            _acquiringBankClient = new AcquiringBankClient(httpClient, config);
        }

        [Fact]
        public async Task SentPaymentToBank()
        {
            var payment = _fixture.Create<Payment>();
            var bankResponse = _fixture.Create<BankResponse>();
            _httpMessageHandlerProxy.SetResponse(HttpStatusCode.OK, bankResponse);

            await _acquiringBankClient.SendPaymentAsync(payment);

            _httpMessageHandlerProxy.NumberOfCalls.Should().Be(1);
        }

        [Fact]
        public async Task ReturnBankResponse()
        {
            var payment = _fixture.Create<Payment>();
            var bankResponse = _fixture.Create<BankResponse>();
            _httpMessageHandlerProxy.SetResponse(HttpStatusCode.OK, bankResponse);

            var response = await _acquiringBankClient.SendPaymentAsync(payment);

            response.Should().BeEquivalentTo(bankResponse);
        }
    }
}
