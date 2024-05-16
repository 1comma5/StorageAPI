using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Models;
using System.ComponentModel;
using StorageAPI.Scripts.Controllers;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Contracts;

namespace StorageAPI.Scripts.Services
{
    public class SettingsService
    {
        private readonly StorageDbContext _context;

        public SettingsService(StorageDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetValues()
        {
            var employee = await _context.Employees.FirstOrDefaultAsync();
            if (employee == null)
            {
                employee = new Employee()
                {
                    FirstName = "Максим",
                    LastName = "Казаков",
                    Phone = "89229821901",
                    Position = "Склад"
                };
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
            }

            var warehouse = await _context.Warehouses.FirstOrDefaultAsync();
            if (warehouse == null)
            {
                warehouse = new Warehouse()
                {
                    Name = "Склад 1",
                    Address = "Анжелия Михеева",
                    Code = "1",
                    Description = "Небольшой",
                    WarehouseType = ""
                };
                await _context.Warehouses.AddAsync(warehouse);
                await _context.SaveChangesAsync();
            }

            var client = await _context.Clients.FirstOrDefaultAsync();
            if (client == null)
            {
                client = new Client()
                {
                    FirstName = "Михеев",
                    LastName = "Антон",
                    Patronymic = "Владимирович",
                    Phone = "89229890101",
                    Note = "После 19 не отвечает",
                    Address = "Ленина 192",
                    Email = "miheevanvl@mail.ru"
                };
                await _context.Clients.AddAsync(client);
                await _context.SaveChangesAsync();
            }

            var orderStatus = await _context.OrderStatusEnumerable.FirstOrDefaultAsync();
            if (orderStatus == null)
            {
                orderStatus = new OrderStatus()
                {
                    Name = "В работе",
                    Description = "Скоро будет готов"
                };
                await _context.OrderStatusEnumerable.AddAsync(orderStatus);
                await _context.SaveChangesAsync();
            }

            var order = await _context.Orders.FirstOrDefaultAsync();
            if (order == null)
            {
                order = new Order()
                {
                    Client = client,
                    Employee = employee,
                    ExecutionDate = DateTime.UtcNow,
                    OrderStatus = orderStatus
                };
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();
            }

            var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync();
            if (manufacturer == null)
            {
                manufacturer = new Manufacturer()
                {
                    Description = "Пластинки",
                    Name = "ЗАО ХОФФМАН"
                };
                await _context.Manufacturers.AddAsync(manufacturer);
                await _context.SaveChangesAsync();
            }

            var unitOfMeasures = await _context.UnitOfMeasures.FirstOrDefaultAsync();
            if (unitOfMeasures == null)
            {
                unitOfMeasures = new UnitOfMeasure()
                {
                    Name = "Штуки",
                    Abbreviation = "шт"
                };
                await _context.UnitOfMeasures.AddAsync(unitOfMeasures);
                await _context.SaveChangesAsync();
            }

            var category = await _context.Categories.FirstOrDefaultAsync();
            if (category == null)
            {
                category = new Category()
                {
                    Name = "Пластины",
                    Description = "Лист из твердого сплава"
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
            }

            var product = await _context.Products.FirstOrDefaultAsync();
            if (product == null)
            {
                product = new Product()
                {
                    Name = "Пластина",
                    Description = "RG",
                    ArticleCode = 31,
                    AdditionalNumber = "1",
                    Manufacturer = manufacturer,
                    UnitOfMeasure = unitOfMeasures,
                    Category = category
                };
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }

            var orderProduct = await _context.OrderProducts.FirstOrDefaultAsync();
            if (orderProduct == null)
            {
                orderProduct = new OrderProduct()
                {
                    Order = order,
                    Product = product,
                    Quantity = 10
                };
                await _context.OrderProducts.AddAsync(orderProduct);
                await _context.SaveChangesAsync();
            }

            var productCost = await _context.ProductCosts.FirstOrDefaultAsync();
            if (productCost == null)
            {
                productCost = new ProductCost()
                {
                    ProductId = 1,
                    ModificationDate = DateTime.UtcNow,
                    Product = product,
                    Cost = 1000
                };
                await _context.ProductCosts.AddAsync(productCost);
                await _context.SaveChangesAsync();
            }

            var storageParameters = await _context.StorageParametersEnumerable.FirstOrDefaultAsync();
            if (storageParameters == null)
            {
                storageParameters = new StorageParameters()
                {
                    Width = 100,
                    Height = 100,
                    Depth = 100,
                    LocationAddress = "1",
                    LocationType = "Коробка"
                };
                await _context.StorageParametersEnumerable.AddAsync(storageParameters);
                await _context.SaveChangesAsync();
            }   

            var storageLocations = await _context.StorageLocations.FirstOrDefaultAsync();
            if (storageLocations == null)
            {
                storageLocations = new StorageLocation()
                {
                    StorageParametersId = 1,
                    StorageParameters = storageParameters,
                    Description = "",
                    Name = "Место хранения №1",
                    WarehouseId = 1,
                    Warehouse = warehouse,
                    Settings = "Стеллаж"
                };
                await _context.StorageLocations.AddAsync(storageLocations);
                await _context.SaveChangesAsync();
            }

            var contactPerson = await _context.ContactPersons.FirstOrDefaultAsync();
            if (contactPerson == null)
            {
                contactPerson = new ContactPerson()
                {
                    FirstName = "Иван",
                    LastName = "Иванов",
                    Description = "Директор"
                };
            }

            var supplier = await _context.Suppliers.FirstOrDefaultAsync();
            if (supplier == null)
            {
                supplier = new Supplier()
                {
                    Name = "Искра",
                    Phone = "89229829898",
                    Email = "iskra@mail.ru",
                    Address = "Россия,город Москва, адрес Ворошиловский проезд 3/4",
                    ContactPersonId = 1,
                    ContactPerson = contactPerson
                };
                await _context.Suppliers.AddAsync(supplier);
                await _context.SaveChangesAsync();
            }

            var operationType = await _context.OperationTypes.FirstOrDefaultAsync();
            if (operationType == null)
            {
                operationType = new OperationType()
                {
                    Name = "Поступление",
                };
                await _context.OperationTypes.AddAsync(operationType);
                await _context.SaveChangesAsync();
            }

            var storageLocationProducts = await _context.StorageLocationProducts.FirstOrDefaultAsync();
            if (storageLocationProducts == null)
            {
                storageLocationProducts = new StorageLocationProduct()
                {
                    ProductId = 1,
                    Product = product,
                    StorageLocationId = 1,
                    StorageLocation = storageLocations,
                    ArrivalDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow,
                    ProductionDate = DateTime.UtcNow,
                    Quantity = 10,
                    SupplierId = 1,
                    Supplier = supplier
                };
                await _context.StorageLocationProducts.AddAsync(storageLocationProducts);
                await _context.SaveChangesAsync();
            }

            var productHistory = await _context.ProductHistories.FirstOrDefaultAsync();
            if (productHistory == null)
            {
                productHistory = new ProductHistory()
                {
                    Date = DateTime.UtcNow,
                    OperationTypeId = 1,
                    OperationType = operationType,
                    StorageLocationId = 1,
                    StorageLocation = storageLocations,
                    StorageLocationProductId = 1,
                    StorageLocationProduct = storageLocationProducts,
                    Quantity = 10
                };
                await _context.ProductHistories.AddAsync(productHistory);
                await _context.SaveChangesAsync();
            }

            return true;
        }
    }
}
