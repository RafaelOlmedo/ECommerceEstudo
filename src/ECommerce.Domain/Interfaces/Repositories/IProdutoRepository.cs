using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> RecuperaTodos();
        Produto Adiciona(Produto produto);
    }
}
