using Ecommerce.TopShelfService.Entities;
using Ecommerce.TopShelfService.Logs;
using Ecommerce.TopShelfService.Schedules;
using ECommerce.ControleLogs;
using Serilog;
using Serilog.Filters;
using Topshelf;


namespace Ecommerce.TopShelfService.AppSettings
{
    public static class TopShelfConfiguracoes
    {
        public static void Init(InformacoesServico informacoesServico) 
        {
			try
			{
                var servico = HostFactory.Run(configuracoes =>
                {
                    configuracoes.Service<AgendamentoJobsIntegracao>(servico =>
                    {
                        servico.ConstructUsing(construtor => new AgendamentoJobsIntegracao());
                        servico.WhenStarted(servicoQuartz => servicoQuartz.IniciaAgendamento(informacoesServico));
                        servico.WhenStopped(servicoQuartz => servicoQuartz.PararAgendamento());
                    });
                    configuracoes.RunAsLocalSystem();
                    configuracoes.StartAutomatically();

                    configuracoes.EnableServiceRecovery(configurador => configurador.RestartService(0));

                    //List<ConfiguracaoLogEntity> configuracaoLogEntities = new();
                    //configuracaoLogEntities.Add(new ExportacaoProdutosLog());
                    //configuracaoLogEntities.Add(new ExportacaoCategoriasLog());

                    //var logSerilog = ConfiguracaoLog.ConfiguracaoInicialSeriLogTexto(configuracaoLogEntities);

                    //var exportacaoProdutosLog = new ExportacaoProdutosLog();
                    //var logSerilog = ConfiguracaoLog.ConfiguracaoInicialSeriLogTexto(exportacaoProdutosLog);

                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.Logger(x => x
                            .Filter.ByExcluding(Matching.WithProperty(nameof(ExportacaoProdutosLog.Tag)))
                            .Filter.ByExcluding(Matching.WithProperty(nameof(ExportacaoCategoriasLog.Tag)))
                                .WriteTo.File(
                                    Path.Combine(
                                        AppParams.CaminhoPastaDeLog,
                                        "log.txt"
                                    ),
                                    rollingInterval: RollingInterval.Infinite))
                        .MinimumLevel.Information()
                        .Enrich.FromLogContext()

                    .WriteTo.Logger(x => x
                        .Filter.ByIncludingOnly(Matching.WithProperty(nameof(ExportacaoProdutosLog.Tag)))
                            .WriteTo.File(
                                Path.Combine(
                                    AppParams.CaminhoPastaDeLog,
                                    ExportacaoProdutosLog.SubPasta,
                                    $"{ExportacaoProdutosLog.NomeArquivo}.{ExportacaoProdutosLog.TipoArquivo}"
                                    ),
                                    rollingInterval: RollingInterval.Day))
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()

                     .WriteTo.Logger(x => x
                        .Filter.ByIncludingOnly(Matching.WithProperty(nameof(ExportacaoProdutosLog.Tag)))
                            .WriteTo.File(
                                Path.Combine(
                                    AppParams.CaminhoPastaDeLog,
                                    ExportacaoCategoriasLog.SubPasta,
                                    $"{ExportacaoCategoriasLog.NomeArquivo}.{ExportacaoCategoriasLog.TipoArquivo}"
                                    ),
                                    rollingInterval: RollingInterval.Day))
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()

#if DEBUG
                    .WriteTo.Console()
#endif
                    .CreateLogger();

                    configuracoes.UseSerilog(Log.Logger);

                    configuracoes.SetServiceName(informacoesServico.NomeServico);
                    configuracoes.SetDisplayName(informacoesServico.NomeExibicaoServico);
                    configuracoes.SetDescription(informacoesServico.DescricaoServico);

                });

                int tipoCodigo = (int)Convert.ChangeType(servico, servico.GetTypeCode());
                Environment.ExitCode = tipoCodigo;
            }
			finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
