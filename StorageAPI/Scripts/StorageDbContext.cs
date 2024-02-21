using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts;

public class StorageDbContext : DbContext
{
    public StorageDbContext(DbContextOptions<StorageDbContext> options) : base(options) { }
    
    public StorageDbContext()
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Client> Clients  { get; set; } = null!;
    public DbSet<ContactPerson> ContactPersons { get; set; } = null!;
    public DbSet<Employee> Employees  { get; set; } = null!;
    public DbSet<EmployeeWarehouse> EmployeeWarehouses { get; set; } = null!;
    public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
    public DbSet<OperationType> OperationTypes  { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = null!;
    public DbSet<OrderStatus> OrderStatusEnumerable { get; set; } = null!;
    public DbSet<Product> Products  { get; set; } = null!;
    public DbSet<ProductCost> ProductCosts  { get; set; } = null!;
    public DbSet<ProductHistory> ProductHistories  { get; set; } = null!;
    public DbSet<StorageLocation> StorageLocations  { get; set; } = null!;
    public DbSet<StorageLocationProduct> StorageLocationProducts  { get; set; } = null!;
    public DbSet<StorageParameters> StorageParametersEnumerable  { get; set; } = null!;
    public DbSet<Supplier> Suppliers  { get; set; } = null!;
    public DbSet<UnitOfMeasure> UnitOfMeasures  { get; set; } = null!;
    public DbSet<Warehouse> Warehouses  { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseNpgsql("Host=26.252.200.44;Port=5432;Database=storage_service;Username=postgres;Password=1"); // Martik
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=storage_service;Username=postgres;Password=1"); // Kaza
        //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=storage_service;Username=postgres;Password=2560"); // Martik-local
    }
}