using Microsoft.EntityFrameworkCore;
using NexusAPI.Contexts;

var builder = WebApplication.CreateBuilder(args);

// registra o DbContext antes do Build()
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.Run();