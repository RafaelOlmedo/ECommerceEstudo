using ECommerce.Domain.Interfaces.Repositories;
using ECommerce.Infra.Data.EntityFramework.Contexts;
using ECommerce.Infra.Data.EntityFramework.Repositories;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ECommerceDataContext>(c => c.UseSqlServer(connectionString));

builder.Services.AddScoped<ECommerceDataContext, ECommerceDataContext>();
builder.Services.AddTransient<IProdutoRepository, ProdutoRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
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
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
