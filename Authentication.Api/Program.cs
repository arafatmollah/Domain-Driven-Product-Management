using System.Text;
using Authentication.Aggregator;
using Authentication.Handler;
using Authentication.Handler.Services;
using Authentication.Repository;
using Authentication.Repository.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServiceBus.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var jwtSection = builder.Configuration.GetSection("Jwt");
var secret     = jwtSection["Secret"]   ?? throw new InvalidOperationException("Jwt:Secret is not configured.");
var issuer     = jwtSection["Issuer"]   ?? throw new InvalidOperationException("Jwt:Issuer is not configured.");
var audience   = jwtSection["Audience"] ?? throw new InvalidOperationException("Jwt:Audience is not configured.");

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
            ValidIssuer              = issuer,
            ValidAudience            = audience,
            IssuerSigningKey         = new SymmetricSecurityKey(
                                           Encoding.UTF8.GetBytes(secret)),
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<UserAggregatorRoot>();

builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();


builder.Services.AddAuthHandlers();

builder.Services.AddServiceBusExtension();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
