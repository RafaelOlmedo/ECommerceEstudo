using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infra.Data.EntityFramework.Contexts
{
    public class ECommerceDataContext : DbContext
    {
        public ECommerceDataContext(DbContextOptions<ECommerceDataContext> options) : base(options)
        {

        }

        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(e =>
            {
                e.HasKey(p => p.Id);
                e.Ignore(p => p.Notifications);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
