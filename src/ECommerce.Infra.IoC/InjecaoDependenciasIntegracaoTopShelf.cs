using ECommerce.Integracao.Domain.Entities;
using ECommerce.Integracao.Domain.Interfaces;
using ECommerce.Integracao.Domain.Interfaces.Services;
using ECommerce.Integracao.Domain.Logs;
using ECommerce.Integracao.Domain.Services;
using ECommerce.Integracao.Domain.Wrapper;
using Microsoft.Extensions.DependencyInjection;


namespace ECommerce.Infra.IoC
{
    public static class InjecaoDependenciasIntegracaoTopShelf
    {
        public static void RegistraDependenciasIntegracaoTopShelf(this IServiceCollection services, 
                                                                  DadosConfiguracaoServico configuracaoServico)
        {
            services.AddTransient<IExportacaoCategoriasService, ExportacaoCategoriasService>();
            services.AddTransient<ISystemIOWrapper, SystemIOWrapper>();

            services.AddSingleton(configuracaoServico);
            services.AddSingleton<ExportacaoCategoriasLog>();
            services.AddSingleton<ExportacaoProdutosLog>();

        }
    }
}
