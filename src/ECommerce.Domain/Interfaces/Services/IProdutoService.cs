using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Produto Adiciona(Produto produto);
    }
}
