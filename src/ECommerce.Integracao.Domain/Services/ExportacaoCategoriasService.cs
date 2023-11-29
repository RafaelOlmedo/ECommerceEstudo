using ECommerce.Integracao.Domain.Entities;
using ECommerce.Integracao.Domain.Interfaces.Services;
using ECommerce.Integracao.Domain.Logs;

namespace ECommerce.Integracao.Domain.Services
{
    public class ExportacaoCategoriasService : IExportacaoCategoriasService
    {
        private readonly DadosConfiguracaoServico _configuracaoServico;
        private readonly ExportacaoCategoriasLog _exportacaoCategoriasLog;

        public ExportacaoCategoriasService(ExportacaoCategoriasLog exportacaoCategoriasLog, 
                                           DadosConfiguracaoServico configuracaoServico)
        {
            _exportacaoCategoriasLog = exportacaoCategoriasLog;
            _configuracaoServico = configuracaoServico;

        }

        public void RealizaExportacaoCategoriasCadastradosEmArquivoJson()
        {
            _exportacaoCategoriasLog.LogInformacao("Cheguei aqui no domínio da integração.");

            _exportacaoCategoriasLog.LogInformacao($"Pasta configurada para a exportação das Categorias: {_configuracaoServico.ParametrosServico.PastaExportacaoCategorias}.");
        }
    }
}
