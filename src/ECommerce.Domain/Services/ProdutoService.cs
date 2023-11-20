using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Domain.Interfaces.Services;

namespace ECommerce.Domain.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaRepository categoriaRepository)
        {
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
        }

        public Produto Adiciona(Produto produto)
        {
            if (!VerificaSeCategoriaExiste(produto))
                return produto;

            produto.RealizaValidacoes();

            if (produto.Invalid)
                return produto;

            produto = _produtoRepository.Adiciona(produto);

            return produto;
        }

        public Produto Atualiza(Produto produto)
        {
            var produtoCadastrado = _produtoRepository.RecuperaPeloId(produto.Id);

            if (produtoCadastrado == null)
            {
                produto.AddNotification(nameof(produto.Id), $"Produto com id: '{produto.Id}' não encontrada.");
                return produto;
            }

            if (!VerificaSeCategoriaExiste(produto))
                return produto;

            produtoCadastrado.AtribuiInformacoesPossiveisParaAtualizacao(produto.Nome, produto.Descricao, produto.Preco, produto.IdCategoria);
            produtoCadastrado.RealizaValidacoes();

            produto = _produtoRepository.Atualiza(produtoCadastrado);

            return produto;
        }

        public (string mensagemErro, bool sucesso) Remove(Guid id)
        {
            var produto = _produtoRepository.RecuperaPeloId(id);

            if (produto == null)
                return ($"Produto com id: '{id}' não encontrada.", false);

            _produtoRepository.Remove(produto);

            return (string.Empty, true);
        }

        private bool VerificaSeCategoriaExiste(Produto produto)
        {
            var categoria = _categoriaRepository.RecuperaPeloId(produto.IdCategoria);

            if (categoria == null)
            {
                produto.AddNotification(nameof(produto.IdCategoria), "Categoria informada não existe.");
                return false;
            }

            return true;
        }
    }
}
