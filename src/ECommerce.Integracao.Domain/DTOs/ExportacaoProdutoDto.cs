using ECommerce.Domain.Entities;

namespace ECommerce.Integracao.Domain.DTOs
{
    public class ExportacaoProdutoDto
    {
        public ExportacaoProdutoDto(Guid id, string nome, string descricao, decimal preco, bool ativo, DateTime dataCriacao, ExportacaoCategoriaDto categoriaDto)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Ativo = ativo;
            DataCriacao = dataCriacao;
            Categoria = categoriaDto;
        }

        public Guid Id { get; set; }
        public string Nome { get; }
        public string Descricao { get; }
        public decimal Preco { get; }
        public bool Ativo { get; }
        public DateTime DataCriacao { get; }

        public ExportacaoCategoriaDto Categoria { get; }

        public static implicit operator ExportacaoProdutoDto(Produto produto) =>
            new ExportacaoProdutoDto
            (
                id: produto.Id,
                descricao: produto.Descricao,
                nome: produto.Nome,
                preco: produto.Preco,
                ativo: produto.Ativo,
                dataCriacao: produto.DataCriacao,
                categoriaDto: new ExportacaoCategoriaDto
                              (
                                    id: produto.Categoria.Id,
                                    nome: produto.Categoria.Nome,
                                    descricao: produto.Categoria.Descricao,
                                    ativo: produto.Categoria.Ativo,
                                    dataCriacao: produto.Categoria.DataCriacao
                              )
            );

        public static List<ExportacaoProdutoDto> ConverteListaDeProdutosEmListaDeExportacaoProdutosDto(List<Produto> produtos) =>
            produtos.Select(produto => (ExportacaoProdutoDto)produto).ToList();

    }
}
