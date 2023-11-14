using ECommerce.Domain.Constants;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infra.Data.EntityFramework.Mappings
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {           
            builder
                .HasKey(x => x.Id);

            builder.Ignore(x => x.Notifications);

            ConfiguracoesColunaNome(builder);
            ConfiguracoesColunaDescricao(builder);

            builder
                .HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.IdCategoria);
        }

        private void ConfiguracoesColunaNome(EntityTypeBuilder<Produto> builder) 
        {
            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(ProdutoTamanhoColunas.Nome);
        }

        private void ConfiguracoesColunaDescricao(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(ProdutoTamanhoColunas.Descricao);
        }
    }
}
