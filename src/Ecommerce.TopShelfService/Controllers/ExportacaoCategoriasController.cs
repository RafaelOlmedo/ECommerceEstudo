using ECommerce.ControleLogs;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.TopShelfService.Controllers
{
    public class ExportacaoCategoriasController
    {
        private readonly IServiceProvider _serviceProvider;
        //private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogService _logService;
        public ExportacaoCategoriasController(IServiceProvider serviceProvider/*,
                                              ILogServiceFactory logServiceFactory*/)
        {
            _serviceProvider = serviceProvider;
            //_categoriaRepository = _serviceProvider.GetRequiredService<ICategoriaRepository>();
            //_logService = logServiceFactory.CriaLogCategorias();
            _logService = _serviceProvider.GetRequiredService<ILogService>();

            IniciaProcesso();
        }

        private void IniciaProcesso()
        {
            _logService.LogInformacao("Iniciando processo de exportação de categorias.", true);
            //var todasCategorias = _categoriaRepository.RecuperaTodos();

            int totalCategorias = 10;
            _logService.LogInformacao($"Foram encontradas {totalCategorias} cadastradas na base de dados");

            _logService.LogInformacao("Finalizando processo de exportação de categorias.", false, true);
        }
    }
}
