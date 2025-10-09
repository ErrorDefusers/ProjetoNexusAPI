using Microsoft.EntityFrameworkCore;
using NexusAPI.Domains;
using Microsoft.OpenApi.Models;
using NexusAPI.Interfaces;
using NexusAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<NexusContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrando Repositories para DI
builder.Services.AddScoped<ICursosRepository, CursosRepository>();
builder.Services.AddScoped<IFerramentasRepository, FerramentasRepository>();
builder.Services.AddScoped<ISetoresRepository, SetoresRepository>();
builder.Services.AddScoped<ITiposFuncionariosRepository, TiposFuncionariosRepository>();
builder.Services.AddScoped<IFuncionarioFerramentasRepository, FuncionariosFerramentasRepository>();
builder.Services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();
builder.Services.AddScoped<IFuncionariosCursosRepository, FuncionariosCursosRepository>();

// JWT Authentication simples
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// Controllers
builder.Services.AddControllers();
builder.Services.AddHttpClient();

// Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API do Nexus",
        Version = "v1",
        Description = "Documentação da API do Nexus"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API do Nexus v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// **Ativando autenticação**
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
