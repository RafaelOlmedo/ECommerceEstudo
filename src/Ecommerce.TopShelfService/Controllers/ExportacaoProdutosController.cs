using Ecommerce.TopShelfService.Logs;
using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.TopShelfService.Controllers
{
    public class ExportacaoProdutosController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILogService _logService;
        public ExportacaoProdutosController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _categoriaRepository = _serviceProvider.GetRequiredService<ICategoriaRepository>();
            _logService = _serviceProvider.GetRequiredService<ILogService>();

            IniciaProcesso();
        }

        private void IniciaProcesso()
        {
            _logService.LogInformacao("Iniciando processo de exportação de produtos.", true);
            var todasCategorias = _categoriaRepository.RecuperaTodos();

            int totalCategorias = todasCategorias.Count();
            _logService.LogInformacao($"Foram encontradas {totalCategorias} cadastradas na base de dados");

            _logService.LogInformacao("Finalizando processo de exportação de produtos.", false, true);
        }    

    }
}
