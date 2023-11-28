using Ecommerce.TopShelfService.Controllers;
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

            return Task.Run(() => new ExportacaoCategoriasController(container));
        }
    }
}
