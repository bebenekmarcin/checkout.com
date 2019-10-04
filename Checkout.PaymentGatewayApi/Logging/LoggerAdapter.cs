using System;
using Microsoft.Extensions.Logging;

namespace Checkout.PaymentGatewayApi.Logging
{
    public class LoggerAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger<T> _logger;

        public LoggerAdapter(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void LogError(Exception ex, params object[] args)
        {
            _logger.LogError(ex, ex.Message, args);
        }

        public void LogInformation(string message)
        {
            _logger.LogInformation(message);
        }
    }
}
