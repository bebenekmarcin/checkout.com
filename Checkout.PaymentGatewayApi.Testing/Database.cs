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
            Payment = fixture.Create<Payment>();

            db.Payments.Add(Payment);
            db.SaveChanges();
        }
    }
}
