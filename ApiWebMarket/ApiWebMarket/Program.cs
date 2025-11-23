using ApiWebMarket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ========== CORS ==========
const string FrontendPolicy = "FrontendPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(FrontendPolicy, policy =>
        policy
            .WithOrigins(
                // Frontend local Vite
                "http://localhost:5173",
                "https://localhost:5173",

                "http://localhost:3000",
                "https://localhost:3000",

                "https://saturated-michelle-unreposeful.ngrok-free.dev"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
    // .AllowCredentials() // solo si usas cookies o fetch con credentials: 'include'
    );
});

// ========== JWT Authentication ==========
var jwtCfg = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtCfg["Issuer"],
            ValidAudience = jwtCfg["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtCfg["Key"]!)
            ),
            ClockSkew = TimeSpan.Zero
        };
    });

// ========== Servicios base ==========
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ========== Entity Framework ==========
builder.Services.AddDbContext<MercaUcaContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MercaUcaContext")
    );
});

builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();

var app = builder.Build();

// ========== Pipeline ==========
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ⚠️ CORS SIEMPRE ANTES DE Auth/Authorization
app.UseCors(FrontendPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
