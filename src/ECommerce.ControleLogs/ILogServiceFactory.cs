using ECommerce.Domain.Interfaces.Services;

namespace ECommerce.ControleLogs
{
    public interface ILogServiceFactory
    {
        ILogService CriaLogProdutos();
        ILogService CriaLogCategorias();
    }
}
