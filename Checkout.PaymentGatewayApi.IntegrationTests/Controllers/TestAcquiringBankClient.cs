﻿using System.Threading.Tasks;
using Checkout.PaymentGatewayApi.Models;
using Checkout.PaymentGatewayApi.Services;

namespace Checkout.PaymentGatewayApi.IntegrationTests.Controllers
{
    public class TestAcquiringBankClient : IAcquiringBankClient
    {
        public Task<BankResponse> SendPaymentAsync(Payment payment)
        {
            return Task.FromResult(new BankResponse());
        }
    }
}
