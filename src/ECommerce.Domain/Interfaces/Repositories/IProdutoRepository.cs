using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        IEnumerable<Produto> RecuperaTodos();
        Produto RecuperaPeloId(Guid id);
        Produto Adiciona(Produto produto);
        Produto Atualiza(Produto produto);
        void Remove(Produto produto);
    }
}
