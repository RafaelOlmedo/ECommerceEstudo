using ECommerce.Domain.Constants;
using ECommerce.Domain.Entities.Base;
using Flunt.Validations;

namespace ECommerce.Domain.Entities
{
    public class Produto : BaseEntity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }


        public Guid IdCategoria { get; private set; }
        public Categoria Categoria { get; private set; }


        public Produto(string nome, string descricao, decimal preco, Guid idCategoria)
        {
            Id = GeraNovoId();
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            IdCategoria = idCategoria;
        }
        public override void RealizaValidacoes()
        {
            AddNotifications(
                 new Contract<Produto>()
                    .Requires()
                    .IsNotNullOrEmpty(Nome, nameof(Nome), $"O campo '{nameof(Nome)}' deve ser preenchido.")
                    .IsNotNullOrEmpty(Descricao, nameof(Descricao), $"O campo '{nameof(Descricao)}' deve ser preenchido.")
                    .IsGreaterThan(Preco, 0, nameof(Preco), $"O campo '{nameof(Preco)}' deve possuir um valor maior do que 0.")
                    .IsLowerThan(Nome.Length, ProdutoTamanhoColunas.Nome, nameof(Nome), $"O campo '{nameof(Nome)}' deve possuir no máximo {ProdutoTamanhoColunas.Nome} caracteres.")
                    .IsLowerThan(Descricao.Length, ProdutoTamanhoColunas.Descricao, nameof(Descricao), $"O campo '{nameof(Descricao)}' deve possuir no máximo {ProdutoTamanhoColunas.Descricao} caracteres.")
                );

            VerificaSeIdCategoriaFoiPreenchido();
        }

        private void VerificaSeIdCategoriaFoiPreenchido()
        {
            if (IdCategoria == Guid.Empty)
                AddNotification(nameof(IdCategoria), $"O campo '{nameof(IdCategoria)}' deve ser preenchido.");
        }

        public void AdicionaDataDeCriacao() =>
            DataCriacao = DateTime.Now;
    }
}
