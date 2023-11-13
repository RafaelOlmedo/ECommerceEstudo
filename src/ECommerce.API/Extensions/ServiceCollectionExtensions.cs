using Microsoft.OpenApi.Models;

namespace ECommerce.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfiguraSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerce API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Olmedo",
                        Email = "rafa-olmedo@hotmail.com",
                        Url = new Uri("https://www.linkedin.com/in/rafael-olmedo-5535834b/")
                    }
                });

                string xmlFile = "ECommerce.API.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
