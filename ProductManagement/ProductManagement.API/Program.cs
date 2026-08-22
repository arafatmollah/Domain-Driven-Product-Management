using Aggregator.Entities;
using Aggregator.Services;
using Aggregator.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManagement.API.Middleware;
using ProductManagement.Handler;
using ProductManagement.Handler.Mapping;
using Repository;
using Repository.Context;

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


builder.Services.AddValidatorsFromAssemblyContaining<ProductValidator>();


builder.Services.AddScoped<IProductRepository, ProductRepository>();


builder.Services.AddScoped<ProductAggregator>();


builder.Services.AddHandlers();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();