using System;
using System.Net.Http;
using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Common;
using Checkout.PaymentGatewayApi.Configuration;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Services
{
    public class AcquiringBankClient : IAcquiringBankClient
    {
        private readonly HttpClient _httpClient;

        public AcquiringBankClient(HttpClient httpClient, AcquiringBankConfig acquiringBankConfig)
        {
            httpClient.BaseAddress = new Uri(acquiringBankConfig.BaseAddress);
            _httpClient = httpClient;
        }

        public async Task<BankResponse> SendPaymentAsync(Payment payment)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/bank", payment);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsJsonAsync<BankResponse>();
            }

            var error = await response.Content.ReadAsStringAsync();

            throw new HttpRequestException(
                $@"StatusCode: {response.StatusCode} 
                        ReasonPhrase: {response.ReasonPhrase} 
                        ResponseContent: {error}");
        }
    }
}
