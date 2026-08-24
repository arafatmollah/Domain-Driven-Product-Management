using Aggregator;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Middleware;
using ProductManagement.Handler;
using ProductManagement.Handler.Mapping;
using Repository;
using Repository.Context;
using ServiceBus.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("DefaultConnection")));


builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<ProductMappingProfile>();
});


builder.Services.AddValidatorsFromAssemblyContaining<ProductAggregatorRoot>();


builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped<ProductAggregatorRoot>();


// Register all command & query handlers
builder.Services.AddHandlers();

// Register the in-process service bus
builder.Services.AddServiceBusExtension();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();