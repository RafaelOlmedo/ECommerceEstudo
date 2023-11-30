using ECommerce.Integracao.Domain.Entities;
using ECommerce.Integracao.Domain.Interfaces.Services;
using ECommerce.Integracao.Domain.Logs;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.TopShelfService.Controllers
{
    public class ExportacaoCategoriasController : ExportacaoCategoriasLog
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IExportacaoCategoriasService _exportacaoCategoriasService;
        private readonly DadosConfiguracaoServico _dadosConfiguracaoServico;
        public ExportacaoCategoriasController(IServiceProvider serviceProvider, DadosConfiguracaoServico dadosConfiguracaoServico)
        {
            _serviceProvider = serviceProvider;
            _exportacaoCategoriasService = _serviceProvider.GetRequiredService<IExportacaoCategoriasService>();
            _dadosConfiguracaoServico = dadosConfiguracaoServico;

            IniciaProcesso();
        }

        private void IniciaProcesso() 
        {
            LogInformacao("Iniciando processamento de exportação das categorias.", true, false);            

            _exportacaoCategoriasService.RealizaExportacaoCategoriasCadastradosEmArquivoJson();

            LogInformacao($"Finalizando o processamento da exportação das categorias.", false, true);
        }
    }
}
