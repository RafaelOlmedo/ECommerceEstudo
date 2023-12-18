using ECommerce.Domain.Constants;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infra.Data.EntityFramework.Mappings
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable(nameof(Categoria));

            builder
                .HasKey(x => x.Id);

            builder.Ignore(x => x.Notifications);

            ConfiguracoesColunaNome(builder);
            ConfiguracoesColunaDescricao(builder);
        }

        private void ConfiguracoesColunaNome(EntityTypeBuilder<Categoria> builder)
        {
            builder.Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(ProdutoTamanhoColunas.Nome);
        }

        private void ConfiguracoesColunaDescricao(EntityTypeBuilder<Categoria> builder)
        {
            builder.Property(x => x.Descricao)
                .IsRequired()
                .HasMaxLength(ProdutoTamanhoColunas.Descricao);
        }
    }
}
