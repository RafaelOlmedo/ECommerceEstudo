using Ecommerce.TopShelfService.Controllers;
using Quartz;

namespace Ecommerce.TopShelfService.Jobs
{
    [DisallowConcurrentExecution]
    public class JobExportacaoProdutos : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var jobDataMap = context.JobDetail.JobDataMap;
            var container = (IServiceProvider)jobDataMap["container"];

            return Task.Run(() => new ExportacaoProdutosController(container));
        }
    }
}
