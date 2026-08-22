
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

// Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Aggregator
builder.Services.AddScoped<ProductAggregator>();

// Handlers
builder.Services.AddScoped<CreateProductHandler>();
builder.Services.AddScoped<GetProductsHandler>();
builder.Services.AddScoped<GetProductByIdHandler>();
builder.Services.AddScoped<UpdateProductHandler>();
builder.Services.AddScoped<DeleteProductHandler>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();