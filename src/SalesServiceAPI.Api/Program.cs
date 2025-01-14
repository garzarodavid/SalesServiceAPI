using Microsoft.EntityFrameworkCore;
using SalesServiceAPI.Api.Filters;
using SalesServiceAPI.Api.Middleware;
using SalesServiceAPI.Infrastructure.Data.Context;
using SalesServiceAPI.IoC;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

IoCConfig.ConfigureServices(builder.Services, builder.Configuration);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                         sqlOptions => sqlOptions.MigrationsAssembly("SalesServiceAPI.Infrastructure")));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>(); 
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>(); 

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial  class Program { } 