using Microsoft.EntityFrameworkCore;
using NexusAPI.Domains;
using Microsoft.OpenApi.Models;
using NexusAPI.Interfaces;
using NexusAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<NexusContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrando Repositories para DI
builder.Services.AddScoped<ICursosRepository, CursosRepository>();
builder.Services.AddScoped<IFerramentasRepository, FerramentasRepository>();
builder.Services.AddScoped<IFuncionariosCursosRepository, FuncionariosCursosRepository>();

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API do Nexus v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
