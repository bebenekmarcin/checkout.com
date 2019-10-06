// ReSharper disable InconsistentNaming

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Checkout.PaymentGatewayApi.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        GPB = 0,
        EUR = 1
    }
}