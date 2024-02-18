using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts;
using StorageAPI.Scripts.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StorageDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=storage_service;Username=postgres;Password=2560");
    // options.UseNpgsql("Host=localhost;Port=5432;Database=storage_service;Username=postgres;Password=2560");
    // options.UseNpgsql("Host=localhost;Port=5432;Database=storage_service;Username=postgres;Password=2560");
});

// Add services
builder.Services.AddScoped<CategoryService>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();