using ECommerce.Domain.Entities;

namespace ECommerce.API.InputModels
{
    public class ProdutoInputModel
    {
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public Guid IdCategoria { get; set; }

        public static implicit operator Produto(ProdutoInputModel input) =>
            new(nome: input.Nome, descricao: input.Descricao, preco: input.Preco, idCategoria: input.IdCategoria);
    }
}
