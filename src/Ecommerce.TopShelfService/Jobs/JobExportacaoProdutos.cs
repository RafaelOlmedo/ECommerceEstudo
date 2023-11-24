﻿using Ecommerce.TopShelfService.Controllers;
using ECommerce.ControleLogs;
using Quartz;

namespace Ecommerce.TopShelfService.Jobs
{
    [DisallowConcurrentExecution]
    public class JobExportacaoProdutos : IJob
    {
        //private readonly ILogServiceFactory _logServiceFactory;

        //public JobExportacaoProdutos(ILogServiceFactory logServiceFactory)
        //{
        //    _logServiceFactory = logServiceFactory;
        //}
        public Task Execute(IJobExecutionContext context)
        {
            var jobDataMap = context.JobDetail.JobDataMap;
            var container = (IServiceProvider)jobDataMap["container"];

            return Task.Run(() => new ExportacaoProdutosController(container/*, _logServiceFactory*/));
        }
    }
}
