using Serilog;
using Serilog.Events;
using Serilog.Filters;

namespace ECommerce.ControleLogs
{
    public static class ConfiguracaoLog
    {
        public static ILogger ConfiguracaoInicialSeriLogTexto(ConfiguracaoLogEntity configuracaoLogEntity)
        {
            Log.Logger = new LoggerConfiguration()
                       .WriteTo.Logger(x => x
                           .Filter.ByExcluding(Matching.WithProperty(nameof(ConfiguracaoLogEntity.Tag)))
                               .WriteTo.File(
                                   Path.Combine(
                                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"), // TODO: Arrumar.
                                       "log.txt"
                                   ),
                                   rollingInterval: RollingInterval.Infinite))
                       .MinimumLevel.Information()
                       .Enrich.FromLogContext()

                       .WriteTo.Logger(x => x
                           .Filter.ByIncludingOnly(Matching.WithProperty(nameof(ConfiguracaoLogEntity.Tag)))
                               .WriteTo.File(
                                   Path.Combine(
                                       Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"),
                                       configuracaoLogEntity.SubPasta,
                                       $"{configuracaoLogEntity.NomeArquivo}.{configuracaoLogEntity.TipoArquivo}"
                                       ),
                                       rollingInterval: RollingInterval.Day))
                       .MinimumLevel.Information()
                       .Enrich.FromLogContext()

#if DEBUG
                    .WriteTo.Console()
#endif
                    .CreateLogger();

            return Log.Logger;
        }

        public static ILogger ConfiguracaoInicialSeriLogTexto(List<ConfiguracaoLogEntity> configuracaoLogEntity)
        {
            // TODO: Isso não está funcionando.
            var loggerConfig = new LoggerConfiguration()
              .WriteTo.Console()
              .WriteTo.File(
                  Path.Combine(
                      Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"),
                      "log.txt"
                  ),
                  rollingInterval: RollingInterval.Infinite
              )
              .MinimumLevel.Information()
              .Enrich.FromLogContext();

                    foreach (var configEntity in configuracaoLogEntity)
                    {
                        Func<LogEvent, bool> includePredicate = evt =>
                        {
                            return evt.Properties.TryGetValue("Tag", out var tag) &&
                                   tag.ToString() == configEntity.Tag;
                        };

                        loggerConfig = loggerConfig.WriteTo.Logger(x => x
                            .Filter.ByIncludingOnly(includePredicate)
                            .WriteTo.File(
                                Path.Combine(
                                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"),
                                    configEntity.SubPasta,
                                    $"{configEntity.NomeArquivo}.{configEntity.TipoArquivo}"
                                ),
                                rollingInterval: RollingInterval.Day
                            )
                        );
                    }

            return loggerConfig.CreateLogger();
        }
    }
}
