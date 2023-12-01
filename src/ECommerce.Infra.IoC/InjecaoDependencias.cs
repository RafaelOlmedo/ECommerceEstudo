using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Domain.Interfaces.Services;
using ECommerce.Domain.Services;
using ECommerce.Infra.Data.EntityFramework.Contexts;
using ECommerce.Infra.Data.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infra.IoC
{
    public static class InjecaoDependencias
    {
        public static void RegistraDependencias(this IServiceCollection services, string stringConexaoEntityFramework = "")
        {
            services.AddTransient<ECommerceDataContext, ECommerceDataContext>();

            if(!string.IsNullOrEmpty(stringConexaoEntityFramework))
                services.AddDbContext<ECommerceDataContext>(options => options.UseSqlServer(stringConexaoEntityFramework));

            services.AddTransient<IProdutoRepository, ProdutoRepository>();
            services.AddTransient<ICategoriaRepository, CategoriaRepository>();

            services.AddTransient<IProdutoService, ProdutoService>();
            services.AddTransient<ICategoriaService, CategoriaService>();
        }     
    }
}