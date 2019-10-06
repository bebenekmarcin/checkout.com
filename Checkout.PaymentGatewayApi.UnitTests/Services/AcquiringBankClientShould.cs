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
        public async Task PostPayment()
        {
            var payment = _fixture.Create<Payment>();
            _httpMessageHandlerProxy.SetResponse(HttpStatusCode.OK, payment);

            await _acquiringBankClient.SendPaymentAsync(payment);

            _httpMessageHandlerProxy.NumberOfCalls.Should().Be(1);
        }

    }
}
