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
    // options.UseNpgsql("Host=localhost;Port=5432;Database=storage_service;Username=postgres;Password=1");
});

// Add services
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<ClientService>();
builder.Services.AddScoped<ContactPersonService>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<EmployeeWarehouseService>();
builder.Services.AddScoped<ManufacturerService>();
builder.Services.AddScoped<OperationTypeService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderProductService>();
builder.Services.AddScoped<OrderStatusService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductCostService>();
builder.Services.AddScoped<ProductHistoryService>();
builder.Services.AddScoped<StorageLocationService>();
builder.Services.AddScoped<StorageLocationProductService>();
builder.Services.AddScoped<StorageParametersService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<UnitOfMeasureService>();
builder.Services.AddScoped<WarehouseService>();


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