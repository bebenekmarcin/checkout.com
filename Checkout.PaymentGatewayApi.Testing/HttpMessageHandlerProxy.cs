using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Checkout.PaymentGatewayApi.Testing
{
    public class HttpMessageHandlerProxy : HttpMessageHandler
    {
        private HttpResponseMessage _responseMessage;

        public int NumberOfCalls { get; private set; }
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            NumberOfCalls++;
            return _responseMessage;
        }

        public void SetResponse<T>(HttpStatusCode statusCode, T data)
        {
            var dataAsString = JsonConvert.SerializeObject(data);
            var content = new StringContent(dataAsString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            _responseMessage = new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = content
            };
        }
    }
}
