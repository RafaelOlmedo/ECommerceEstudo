using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Infra.Data.EntityFramework.Contexts;
using ECommerce.Infra.Data.EntityFramework.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infra.IoC
{
    public class InjecaoDependencias
    {
        public static void RegistraDependencias(IServiceCollection services)
        {
            services.AddScoped<ECommerceDataContext, ECommerceDataContext>();
            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();
        }
    }
}