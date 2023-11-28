using Ecommerce.TopShelfService.Logs;
using ECommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.TopShelfService.Controllers
{
    public class ExportacaoCategoriasController : ExportacaoCategoriasLog
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICategoriaRepository _categoriaRepository;

        public ExportacaoCategoriasController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _categoriaRepository = _serviceProvider.GetRequiredService<ICategoriaRepository>();

            IniciaProcesso();
        }

        private void IniciaProcesso() 
        {
            LogInformacao("Iniciando processamento de exportação das categorias.", true, false);

            int quantidadeCategorias = 20;

            LogInformacao($"Quantidade de categorias encontradas: {quantidadeCategorias}");

            LogInformacao($"Finalizando o processamento da exportação das categorias.", false, true);
        }
    }
}
