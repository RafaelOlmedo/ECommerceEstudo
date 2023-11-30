using Ecommerce.TopShelfService.Constants;
using Ecommerce.TopShelfService.Controllers;
using ECommerce.Integracao.Domain.Entities;
using Quartz;

namespace Ecommerce.TopShelfService.Jobs
{
    [DisallowConcurrentExecution]
    public class JobExportacaoProdutos : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobDataMap = context.JobDetail.JobDataMap;
            var container = (IServiceProvider)jobDataMap[ConfiguracoesJobConstantes.Container];
            var configuracoesServico = (DadosConfiguracaoServico)jobDataMap[ConfiguracoesJobConstantes.ConfiguracoesServico];

            return Task.Run(() => new ExportacaoProdutosController(container, configuracoesServico));
        }
    }
}
