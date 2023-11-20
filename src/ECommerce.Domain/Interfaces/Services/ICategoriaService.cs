using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Interfaces.Services
{
    public interface ICategoriaService
    {
        Categoria Adiciona(Categoria categoria);
        Categoria Atualiza(Categoria categoria);
        (string mensagemErro, bool sucesso) Remove(Guid id);
    }
}
