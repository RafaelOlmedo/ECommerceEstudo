using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Services
{
    public interface IProdutoService
    {
        Produto Adiciona(Produto produto);
        Produto Atualiza(Produto produto);
        (string mensagemErro, bool sucesso) Remove(Guid id);
    }
}
