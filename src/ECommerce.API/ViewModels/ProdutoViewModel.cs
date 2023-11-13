using ECommerce.Domain.Entities;

namespace ECommerce.API.ViewModels
{
    public class ProdutoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }

        public static implicit operator ProdutoViewModel(Produto produto) =>
            new ProdutoViewModel()
            {
                Nome = produto.Nome,
                Ativo = produto.Ativo,
                DataCriacao = produto.DataCriacao,
                Descricao = produto.Descricao,
                Id = produto.Id,
                Preco = produto.Preco
            };

        public static IEnumerable<ProdutoViewModel> ConverteListaDeProdutoEmListaProdutoViewModel(IEnumerable<Produto> produtos)
        {
            if(produtos == null)
                return Enumerable.Empty<ProdutoViewModel>();

            return produtos.Select(produto => (ProdutoViewModel)produto);
        }
    }
}
