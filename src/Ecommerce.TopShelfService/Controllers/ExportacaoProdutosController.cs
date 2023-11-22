using ECommerce.Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.TopShelfService.Controllers
{
    public class ExportacaoProdutosController
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICategoriaRepository _categoriaRepository;
        public ExportacaoProdutosController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _categoriaRepository = _serviceProvider.GetRequiredService<ICategoriaRepository>();

            IniciaProcesso();
        }

        private void IniciaProcesso()
        {
            var todasCategorias = _categoriaRepository.RecuperaTodos();

            int totalCategorias = todasCategorias.Count();

            Console.WriteLine($"Quantidade Total de Categorias: {totalCategorias}.");
        }
    }
}
