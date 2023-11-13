using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> RecuperaTodos();
        Categoria Adiciona(Categoria categoria);
    }
}
