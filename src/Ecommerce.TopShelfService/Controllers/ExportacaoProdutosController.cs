using ECommerce.Integracao.Domain.Entities;
using ECommerce.Integracao.Domain.Interfaces.Services;
using ECommerce.Integracao.Domain.Logs;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.TopShelfService.Controllers
{
    public class ExportacaoProdutosController : ExportacaoProdutosLog
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExportacaoProdutosService _exportacaoProdutosService;
        private readonly DadosConfiguracaoServico _dadosConfiguracaoServico;

        public ExportacaoProdutosController(IServiceProvider serviceProvider,
                                            DadosConfiguracaoServico dadosConfiguracaoServico)
        {
            _serviceProvider = serviceProvider;
            _exportacaoProdutosService = _serviceProvider.GetRequiredService<IExportacaoProdutosService>();
            _dadosConfiguracaoServico = dadosConfiguracaoServico;

            IniciaProcesso();
        }

        private void IniciaProcesso()
        {
            LogInformacao("Iniciando processo de exportação de produtos.", true);

            _exportacaoProdutosService.RealizaExportacaoProdutosCadastradosEmArquivoJson();

            LogInformacao("Finalizando processo de exportação de produtos.", false, true);
        }    

    }
}
