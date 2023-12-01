using ECommerce.API.Extensions;
using ECommerce.Infra.Data.EntityFramework.Contexts;
using ECommerce.Infra.IoC;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

// TODO: Possibilidade de melhoria: Incluir para que cada controller tenha um arquivo separado de log.
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Error()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs"),
                    "log.txt"
                ), rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

string stringConexao = builder.Configuration.GetConnectionString("DefaultConnection");
InjecaoDependencias.RegistraDependencias(builder.Services, stringConexao);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

ServiceCollectionExtensions.ConfiguraSwagger(builder.Services);

var app = builder.Build();
app.UseCors(option => option.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());

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

app.UseDefaultFiles();
app.UseStaticFiles();
