using Ecommerce.TopShelfService.Logs;
using ECommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.TopShelfService.Controllers
{
    public class ExportacaoProdutosController : ExportacaoProdutosLog
    {
        private readonly IServiceProvider _serviceProvider;
        //private readonly ICategoriaRepository _categoriaRepository;
        public ExportacaoProdutosController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //_categoriaRepository = _serviceProvider.GetRequiredService<ICategoriaRepository>();

            IniciaProcesso();
        }

        private void IniciaProcesso()
        {
            LogInformacao("Iniciando processo de exportação de produtos.", true);
            //var todasCategorias = _categoriaRepository.RecuperaTodos();

            int totalCategorias = 10;
            LogInformacao($"Foram encontradas {totalCategorias} cadastradas na base de dados");

            LogInformacao("Finalizando processo de exportação de produtos.", false, true);
        }    

    }
}
