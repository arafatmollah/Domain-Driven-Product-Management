
using Aggregator.Services;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Handler;
using Repository;
using Repository.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ProductAggregator>();

builder.Services.AddScoped<CreateProductHandler>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();