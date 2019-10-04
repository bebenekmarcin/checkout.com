using System;

namespace Checkout.PaymentGatewayApi.Logging
{
    public interface ILoggerAdapter<T>
    {
        void LogInformation(string message);
        void LogError(Exception ex, params object[] args);
    }
}