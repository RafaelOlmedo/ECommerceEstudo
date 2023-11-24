using Ecommerce.TopShelfService.Controllers;
using ECommerce.ControleLogs;
using Quartz;

namespace Ecommerce.TopShelfService.Jobs
{
    [DisallowConcurrentExecution]

    public class JobExportacaoCategorias : IJob
    {
        //private readonly ILogServiceFactory _logServiceFactory;

        //public JobExportacaoCategorias(ILogServiceFactory logServiceFactory)
        //{
        //    _logServiceFactory = logServiceFactory;
        //}
        public Task Execute(IJobExecutionContext context)
        {
            var jobDataMap = context.JobDetail.JobDataMap;
            var container = (IServiceProvider)jobDataMap["container"];

            return Task.Run(() => new ExportacaoCategoriasController(container/*, logServiceFactory*/));
        }
    }
}
