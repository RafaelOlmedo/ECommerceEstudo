using ECommerce.Domain.Entities;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Domain.Interfaces.Services;

namespace ECommerce.Domain.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public Categoria Adiciona(Categoria categoria)
        {
            categoria.RealizaValidacoes();

            if (categoria.Invalid)
                return categoria;

            _categoriaRepository.Adiciona(categoria);
            return categoria;
        }

        public Categoria Atualiza(Categoria categoria)
        {
            var categoriaRecuperada = _categoriaRepository.RecuperaPeloId(categoria.Id);

            if (categoriaRecuperada == null)
            {
                categoria.AddNotification(nameof(categoria.Id), $"Categoria com id: '{categoria.Id}' não encontrada.");
                return categoria;
            }

            categoriaRecuperada.AtribuiNome(categoria.Nome);
            categoriaRecuperada.AtribuiDescricao(categoria.Descricao);

            categoria =_categoriaRepository.Atualiza(categoriaRecuperada);

            return categoria;
        }

        public (string mensagemErro, bool sucesso) Remove(Guid id)
        {
            var categoria = _categoriaRepository.RecuperaPeloId(id);

            if (categoria == null) 
                return ($"Categoria com id: '{id}' não encontrada.", false);

            _categoriaRepository.Remove(categoria);

            return (string.Empty, true);
        }


    }
}
