using Serilog;
using Serilog.Sinks.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesServiceAPI.Application.Interfaces.Services;
using SalesServiceAPI.Application.Services;
using SalesServiceAPI.Domain.Repositories;
using SalesServiceAPI.Infrastructure.Repositories;

namespace SalesServiceAPI.IoC
{
    public class IoCConfig
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // Configurar Serilog com RabbitMQ
            var rabbitMqConfig = new RabbitMQClientConfiguration
            {
                Hostnames = { "localhost" },  // Endereço do RabbitMQ
                Port = 5672,
                Username = "guest",
                Password = "guest",
                Exchange = "logs",
                ExchangeType = "direct"
            };

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.RabbitMQ((clientConfiguration, sinkConfiguration) =>
                {
                    clientConfiguration.Hostnames.Add("localhost");
                    clientConfiguration.Port = 5672;
                    clientConfiguration.Username = "guest";
                    clientConfiguration.Password = "guest";
                    clientConfiguration.Exchange = "logs";
                    clientConfiguration.ExchangeType = "direct";
                    sinkConfiguration.TextFormatter = new Serilog.Formatting.Json.JsonFormatter();
                })
                .CreateLogger();

            services.AddSingleton(Log.Logger);

            // Registrar serviços
            services.AddScoped<IVendaService, VendaService>();

            // Registrar repositórios
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFilialRepository, FilialRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Registrar AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}