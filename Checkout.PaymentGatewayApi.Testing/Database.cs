using AutoFixture;
using Checkout.PaymentGatewayApi.Database;
using Checkout.PaymentGatewayApi.Models;

namespace Checkout.PaymentGatewayApi.Testing
{
    public class Database
    {
        public Payment Payment;

        public void InitializeDbForTests(PaymentDbContext db)
        {
            Fixture fixture = new Fixture();
            Payment = fixture.Build<Payment>()
                .With(p => p.CardNumber, "4111111111111111").Create();

            db.Payments.Add(Payment);
            db.SaveChanges();
        }
    }
}
