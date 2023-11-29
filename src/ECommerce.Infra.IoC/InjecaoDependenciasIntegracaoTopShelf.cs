using ECommerce.Integracao.Domain.Entities;
using ECommerce.Integracao.Domain.Interfaces.Services;
using ECommerce.Integracao.Domain.Logs;
using ECommerce.Integracao.Domain.Services;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.Infra.IoC
{
    public static class InjecaoDependenciasIntegracaoTopShelf
    {
        public static void RegistraDependenciasIntegracaoTopShelf(this IServiceCollection services, 
                                                                  DadosConfiguracaoServico configuracaoServico)
        {
            services.AddTransient<IExportacaoCategoriasService, ExportacaoCategoriasService>();
            services.AddSingleton(configuracaoServico);
            services.AddSingleton<ExportacaoCategoriasLog>();
            services.AddSingleton<ExportacaoProdutosLog>();

        }
    }
}
