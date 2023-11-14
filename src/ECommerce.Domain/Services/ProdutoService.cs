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
            var categoria = _categoriaRepository.RecuperaPeloId(produto.IdCategoria);

            if (categoria == null)
            {
                produto.AddNotification(nameof(produto.IdCategoria), "Categoria informada não existe.");
                return produto;
            }

            produto.RealizaValidacoes();

            if (produto.Invalid)
                return produto;

            produto = _produtoRepository.Adiciona(produto);

            return produto;
        }
    }
}
