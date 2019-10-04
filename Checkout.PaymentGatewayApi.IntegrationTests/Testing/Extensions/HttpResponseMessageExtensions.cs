using System.Net.Http;
using FluentAssertions;

namespace Checkout.PaymentGatewayApi.IntegrationTests.Testing.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static void EnsureSuccessStatusCodeAndExpectedContent(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                response.Content.Headers.ContentType.ToString().Should().Be("application/json; charset=utf-8");
            else
            {
                var error = response.Content.ReadAsStringAsync().Result;
                throw new HttpRequestException(
                    $@"StatusCode: {response.StatusCode} 
                            ReasonPhrase: {response.ReasonPhrase} 
                            ResponseContent: {error}");
            }
        }
    }
}
