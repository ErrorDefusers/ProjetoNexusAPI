using Microsoft.EntityFrameworkCore;
using NexusAPI.Domains;
using Microsoft.OpenApi.Models;
using NexusAPI.Interfaces;
using NexusAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<NexusContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICursosRepository, CursosRepository>();
builder.Services.AddScoped<IFerramentasRepository, FerramentasRepository>();
builder.Services.AddScoped<ISetoresRepository, SetoresRepository>();
builder.Services.AddScoped<ITiposFuncionariosRepository, TiposFuncionariosRepository>();
builder.Services.AddScoped<IFuncionarioFerramentasRepository, FuncionariosFerramentasRepository>();
builder.Services.AddScoped<IFuncionariosRepository, FuncionariosRepository>();
builder.Services.AddScoped<IFuncionariosCursosRepository, FuncionariosCursosRepository>();


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


builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); 
    });
});


builder.Services.AddControllers();
builder.Services.AddHttpClient();


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Minha API do Nexus",
        Version = "v1",
        Description = "Documentação da API do Nexus"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite 'Bearer' [espaço] e depois seu token.\n\nExemplo: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
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


app.UseCors("CorsPolicy");

app.UseStaticFiles();

app.MapControllers();

app.Run();
