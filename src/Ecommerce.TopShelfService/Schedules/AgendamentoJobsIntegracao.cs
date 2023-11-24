using Ecommerce.TopShelfService.Controllers;
using Ecommerce.TopShelfService.Entities;
using Ecommerce.TopShelfService.Jobs;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Infra.Data.EntityFramework.Contexts;
using ECommerce.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Serilog;
using System.Collections.Specialized;
using Ecommerce.TopShelfService.Logs;
using ECommerce.ControleLogs;

namespace Ecommerce.TopShelfService.Schedules

{
    public class AgendamentoJobsIntegracao
    {
        private readonly IScheduler _gerenciadorAgendamento;

        private int
            _intervaloExportacaoProdutosEmMinutos = 1,
            _intervaloExportacaoCategoriasEmMinutos = 1;

        public AgendamentoJobsIntegracao()
        {
            NameValueCollection collection = new NameValueCollection
            {
                { "quartz.scheduler.instanceName", "ECommerceScheduler" },
                { "quartz.serializer.type", "binary" },
                { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
                { "quartz.threadPool.threadCount", "3" },
                { "quartz.jobStore.misfireThreshold", "60000"}
            };

            var agendamento = new StdSchedulerFactory(collection);
            _gerenciadorAgendamento = agendamento.GetScheduler().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public void IniciaAgendamento(InformacoesServico informacoesServico)
        {
            Log.Information("");
            Log.Information($"Serviço inicializado. Versão: {informacoesServico.VersaoServico}");

            var configuracaoServico = ConfiguracoesServicoController.ObtemConfiguracaoServico();

            if (configuracaoServico.Invalido)
            {
                Log.Error("Existem erros no preenchimento do arquivo de configuração. Segue(m) detalhe(s):");
                foreach (var notificacao in configuracaoServico.Notifications)
                    Log.Error(notificacao.Message);

                return;
            }

            if (configuracaoServico.ParametrosServico.ScheduleExportacaoProdutosEmMinutos > 0)
                _intervaloExportacaoProdutosEmMinutos = configuracaoServico.ParametrosServico.ScheduleExportacaoProdutosEmMinutos;

            //if (configuracaoServico.ParametrosServico.ScheduleExportacaoCategoriasEmMinutos > 0)
            //    _intervaloExportacaoCategoriasEmMinutos = configuracaoServico.ParametrosServico.ScheduleExportacaoCategoriasEmMinutos;

            #region Teste
            var services = ConfiguraServicos();

            #endregion

            _gerenciadorAgendamento.Start().Wait();

            AgendamentoJobs(services);
        }

        private IServiceProvider ConfiguraServicos()
        {
            var services = new ServiceCollection();
            InjecaoDependencias.RegistraDependencias(services);

            string connectionString = @"Server=DESKTOP-OQ2HHAO\SQLEXPRESS2022;Database=ECommerce;User Id=sa;Password=saadmin; Integrated Security=True; trustServerCertificate=true";

            // TODO: Tem que arrumar ainda.
            services.AddDbContext<ECommerceDataContext>(options => options.UseSqlServer(connectionString));

            // IoC do Log. Provavelmente tem como melhorar.
            services.AddTransient<ILogService>(sp =>
            {
                return new LogTextoService
                (
                    tag: ExportacaoProdutosLog.Tag,
                    subPasta: ExportacaoProdutosLog.SubPasta,
                    nomeArquivo: ExportacaoProdutosLog.NomeArquivo,
                    tipoArquivo: ExportacaoProdutosLog.TipoArquivo
                );
            });

            //services.AddTransient<ILogService>(provider =>
            //{
            //    var factory = provider.GetRequiredService<ILogServiceFactory>();
            //    return factory.CriaLogProdutos();
            //});


            return services.BuildServiceProvider();
        }

        private void AgendamentoJobs(IServiceProvider container)
        {
            var jobDataMap = new JobDataMap();
            jobDataMap.Put("container", container);

            IJobDetail jobExportacaoProdutos = JobBuilder
                .Create<JobExportacaoProdutos>()
                .UsingJobData(jobDataMap)
                .Build();

            ITrigger triggerExportacaoProdutos = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(_intervaloExportacaoProdutosEmMinutos).RepeatForever())
                .Build();

            IJobDetail jobExportacaoCategorias = JobBuilder
                .Create<JobExportacaoCategorias>()
                .UsingJobData(jobDataMap)
                .Build();

            ITrigger triggerExportacaoCategorias = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(_intervaloExportacaoCategoriasEmMinutos).RepeatForever())
                .Build();

            _gerenciadorAgendamento.ScheduleJob(jobExportacaoProdutos, triggerExportacaoProdutos).Wait();
            _gerenciadorAgendamento.ScheduleJob(jobExportacaoCategorias, triggerExportacaoCategorias).Wait();
        }

        public void PararAgendamento()
        {
            _gerenciadorAgendamento.Shutdown().Wait();
            Log.Information("Serviço parado.");
        }
    }
}
