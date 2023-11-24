using ECommerce.Domain.Entities;
using ECommerce.Infra.Data.EntityFramework.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
            modelBuilder.ApplyConfiguration(new ProdutoMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());


            base.OnModelCreating(modelBuilder);
        }

        // TODO: Validar se realmente não precisa disso.
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer("DefaultConnection");
        //}
    }
}
