using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoFixture;
using Checkout.PaymentGatewayApi.Common;
using Checkout.PaymentGatewayApi.Models;
using Checkout.PaymentGatewayApi.Services;
using Checkout.PaymentGatewayApi.Testing;
using Checkout.PaymentGatewayApi.Testing.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Checkout.PaymentGatewayApi.IntegrationTests.Controllers
{
    public class PaymentApiShould : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;
        private readonly Fixture _fixture;
        private readonly HttpClient _httpClient;

        public PaymentApiShould(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _fixture = new Fixture();

            _httpClient = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddTransient<IAcquiringBankClient, TestAcquiringBankClient>();
                    });
                })
                .CreateClient();
        }

        [Fact]
        public async Task GET_ReturnSuccessAndCorrectContentType()
        {
            var existingPaymentId = _factory.Database.Payment.Id;

            var response = await _httpClient.GetAsync($"api/payment/{existingPaymentId}");

            response.EnsureSuccessStatusCodeAndExpectedContent();
        }

        [Fact]
        public async Task GET_ReturnPayments()
        {
            var existingPaymentId = _factory.Database.Payment.Id;

            var response = await _httpClient.GetAsync($"api/payment/{existingPaymentId}");

            response.EnsureSuccessStatusCodeAndExpectedContent();
            var payment = await response.Content.ReadAsJsonAsync<Payment>();
            payment.Should().BeEquivalentTo(_factory.Database.Payment, options => options.Excluding(x => x.CardNumber));
        }

        [Fact]
        public async Task POST_ReturnSuccessAndCorrectContentType()
        {
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111 1111 1111 1111").Create();

            var response = await _httpClient.PostAsJsonAsync("api/payment", payment);

            response.EnsureSuccessStatusCodeAndExpectedContent();
        }

        private void ConfigureClient(IServiceProvider arg1, HttpClient arg2)
        {
        }

        [Theory]
        [InlineData("")]
        [InlineData("abcd")]
        [InlineData("4111-1111-1111-aaaa")]
        public async Task POST_ValidateIfCardNumberIsProvided(string notValidCardNumberValue)
        {
            var payment = _fixture.Build<Payment>().With(p => p.CardNumber, notValidCardNumberValue).Create();

            var response = await _httpClient.PostAsJsonAsync("api/payment", payment);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var errorMessage = await response.Content.ReadAsStringAsync();
            errorMessage.Should().Contain("CardNumber");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(2018)]
        public async Task POST_ValidateIfExpiryYearIsInValidRange(int notValidExpiryYearValue)
        {
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111 1111 1111 1111")
                .With(p => p.ExpiryYear, notValidExpiryYearValue).Create();

            var response = await _httpClient.PostAsJsonAsync("api/payment", payment);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var errorMessage = await response.Content.ReadAsStringAsync();
            errorMessage.Should().Contain("ExpiryYear");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(13)]
        public async Task POST_ValidateIfExpiryMonthIsValidRange(int notValidExpiryMonthValue)
        {
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111 1111 1111 1111")
                .With(p => p.ExpiryMonth, notValidExpiryMonthValue).Create();

            var response = await _httpClient.PostAsJsonAsync("api/payment", payment);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var errorMessage = await response.Content.ReadAsStringAsync();
            errorMessage.Should().Contain("ExpiryMonth");
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task POST_ValidateIfAmountIsValidRange(decimal notValidAmountValue)
        {
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111 1111 1111 1111")
                .With(p => p.Amount, notValidAmountValue).Create();

            var response = await _httpClient.PostAsJsonAsync("api/payment", payment);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var errorMessage = await response.Content.ReadAsStringAsync();
            errorMessage.Should().Contain("Amount");
        }

        [Theory]
        [InlineData(99)]
        [InlineData(1000)]
        public async Task POST_ValidateIfCvvIsValid(int notValidCvvValue)
        {
            var payment = _fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111 1111 1111 1111")
                .With(p => p.Cvv, notValidCvvValue).Create();

            var response = await _httpClient.PostAsJsonAsync("api/payment", payment);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var errorMessage = await response.Content.ReadAsStringAsync();
            errorMessage.Should().Contain("Cvv");
        }
    }
}
