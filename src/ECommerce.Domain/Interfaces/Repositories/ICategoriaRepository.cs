using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> RecuperaTodos();
        Categoria RecuperaPeloId(Guid id);
        Categoria Adiciona(Categoria categoria);
    }
}
