using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OrderManagement.Aggregator;
using OrderManagement.Handler;
using OrderManagement.Handler.Mapping;
using OrderManagement.Repository;
using OrderManagement.Repository.Context;
using ServiceBus.Handlers;
using OrderManagement.API.Middleware;

var builder = WebApplication.CreateBuilder(args);
var jwtSection = builder.Configuration.GetSection("Jwt");

var secret = jwtSection["Secret"]
    ?? throw new InvalidOperationException("Jwt:Secret is not configured.");

var issuer = jwtSection["Issuer"]
    ?? throw new InvalidOperationException("Jwt:Issuer is not configured.");

var audience = jwtSection["Audience"]
    ?? throw new InvalidOperationException("Jwt:Audience is not configured.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidateAudience         = true,
            ValidateLifetime         = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer   = issuer,
            ValidAudience = audience,

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(secret))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration
            .GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<OrderMappingProfile>();
});

builder.Services.AddValidatorsFromAssemblyContaining<OrderAggregatorRoot>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<OrderAggregatorRoot>();

builder.Services.AddOrderHandlers();

builder.Services.AddServiceBus();

var app = builder.Build();
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
