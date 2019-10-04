using Checkout.PaymentGatewayApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Checkout.PaymentGatewayApi.Database
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
            : base(options)
        {
        }

        public DbSet<Payment> Payments { get; set; }
    }

}
