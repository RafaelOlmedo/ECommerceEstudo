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
    }
}
