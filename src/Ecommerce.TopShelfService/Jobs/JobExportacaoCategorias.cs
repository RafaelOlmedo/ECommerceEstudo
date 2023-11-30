using Ecommerce.TopShelfService.Controllers;
using ECommerce.Integracao.Domain.Entities;
using Quartz;

namespace Ecommerce.TopShelfService.Jobs
{
    [DisallowConcurrentExecution]
    public class JobExportacaoCategorias : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobDataMap = context.JobDetail.JobDataMap;
            var container = (IServiceProvider)jobDataMap["container"];
            var config = (DadosConfiguracaoServico)jobDataMap["config"];


            return Task.Run(() => new ExportacaoCategoriasController(container, config));
        }
    }
}
