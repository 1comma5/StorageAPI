using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class OrderService
{
    private readonly StorageDbContext _context;

    public OrderService(StorageDbContext context)
    {
        _context = context;
    }

    public async Task<OrderModel?> Get(int id)
    {
        // Получить order по id
        var order = await _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.OrderStatus)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (order == null) return null;

        // Получить orderProducts по order и включить Product
        var orderProducts = await _context.OrderProducts
            .Include(op => op.Product).ThenInclude(product => product.Manufacturer)
            .Include(op => op.Product).ThenInclude(product => product.UnitOfMeasure)
            .Include(op => op.Product).ThenInclude(product => product.Category)
            .Where(op => op.Order == order)
            .ToListAsync();

        var productModels = new ProductModel[orderProducts.Count];
        for (var i = 0; i < orderProducts.Count; i++)
        {
            var orderProduct = orderProducts[i];
            productModels[i] = new ProductModel(
                orderProduct.Product.Id,
                orderProduct.Product.Name,
                orderProduct.Product.Description,
                orderProduct.Product.ArticleCode,
                orderProduct.Product.AdditionalNumber,
                orderProduct.Product.Manufacturer.Id,
                orderProduct.Product.UnitOfMeasure.Id,
                orderProduct.Product.Category.Id,
                orderProduct.Quantity
            );
        }

        return new OrderModel(order.Id, order.Client.Id, order.Employee.Id, order.OrderStatus.Name, productModels);
    }

    public async Task<OrderModel?> Add(OrderModel orderModel)
    {
    // Проверить, что существуют клиент, сотрудник и статус заказа
    var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == orderModel.ClientId && !x.IsDeleted);
    var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == orderModel.EmployeeId && !x.IsDeleted);
    var orderStatus = await _context.OrderStatusEnumerable.FirstOrDefaultAsync(x => x.Name == orderModel.OrderStatus);

    if (orderStatus == null)
    {
    orderStatus = new OrderStatus 
    { 
        Name = orderModel.OrderStatus, 
        Description = "Описание статуса заказа" // Установите значение Description
    };
    _context.OrderStatusEnumerable.Add(orderStatus);
    await _context.SaveChangesAsync(); // Сохраняем новый статус заказа в базу данных
    }

    if (client == null || employee == null) return null;

    // Проверить продукты
    foreach (var productModel in orderModel.Products)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productModel.Id && !x.IsDeleted);
        if (product == null) return null;
    }

    // Создать order
    var order = new Order
    {
        Client = client,
        Employee = employee,
        OrderStatus = orderStatus
    };

    // Добавить продукты в заказ
    foreach (var productModel in orderModel.Products)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productModel.Id && !x.IsDeleted);
        if (product != null)
        {
            var orderProduct = new OrderProduct
            {
                Order = order,
                Product = product,
                Quantity = productModel.Quantity // Предполагается, что OrderProduct имеет свойство Quantity
            };
            _context.OrderProducts.Add(orderProduct); // Добавляем новый OrderProduct в контекст
        }
    }

    _context.Orders.Add(order);
    await _context.SaveChangesAsync(); // Сохраняем новый заказ в базу данных

    return orderModel;
}

        // Создать orderProducts
        foreach (var productModel in orderModel.Products)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productModel.Id && !x.IsDeleted);
            if (product == null) continue;
            var orderProduct = new OrderProduct
            {
                Order = order,
                Product = product,
                Quantity = productModel.Quantity
            };
            await _context.OrderProducts.AddAsync(orderProduct);
        }

        await _context.SaveChangesAsync();

        return await Get(order.Id);
    }


    public async Task<OrderModel?> Update(OrderModel orderModel)
    {
        // Получить order по id
        var order = await _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.OrderStatus)
            .FirstOrDefaultAsync(x => x.Id == orderModel.Id && !x.IsDeleted);

        if (order == null) return null;

        // Получить orderProducts по order и включить Product
        var orderProducts = await _context.OrderProducts
            .Include(op => op.Product).ThenInclude(product => product.Manufacturer)
            .Include(op => op.Product).ThenInclude(product => product.UnitOfMeasure)
            .Include(op => op.Product).ThenInclude(product => product.Category)
            .Where(op => op.Order == order)
            .ToListAsync();

        // Удалить orderProducts
        foreach (var orderProduct in orderProducts)
        {
            _context.OrderProducts.Remove(orderProduct);
        }

        // Проверить, что существуют клиент, сотрудник и статус заказа
        var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == orderModel.ClientId && !x.IsDeleted);
        var employee = await _context.Employees.FirstOrDefaultAsync(x => x.Id == orderModel.EmployeeId && !x.IsDeleted);
        var orderStatus =
            await _context.OrderStatusEnumerable.FirstOrDefaultAsync(x => x.Name == orderModel.OrderStatus);

        if (orderStatus == null)
        {
            orderStatus = new OrderStatus { Name = orderModel.OrderStatus };
            await _context.OrderStatusEnumerable.AddAsync(orderStatus);
        }
        
        if (client == null || employee == null) return null;
        // Проверить продукты
        foreach (var productModel in orderModel.Products)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productModel.Id && !x.IsDeleted);
            if (product == null) return null;
        }

        // Обновить order
        order.Client = client;
        order.Employee = employee;
        order.OrderStatus = orderStatus;

        // Создать orderProducts
        foreach (var productModel in orderModel.Products)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productModel.Id && !x.IsDeleted);
            if (product == null) continue;
            var orderProduct = new OrderProduct
            {
                Order = order,
                Product = product,
                Quantity = productModel.Quantity
            };
            await _context.OrderProducts.AddAsync(orderProduct);
        }

        await _context.SaveChangesAsync();

        return await Get(order.Id);
    }

    public async Task<bool> Delete(int id)
    {
        // Получить order по id
        var order = await _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.OrderStatus)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        
        // Удаление это просто пометка IsDeleted = true
        if (order == null) return false;
        order.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<OrderModel>> GetAll()
    {
        // Получить все order
        var orders = await _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.OrderStatus)
            .Where(x => !x.IsDeleted)
            .ToListAsync();

        var orderModels = new List<OrderModel>();
        foreach (var order in orders)
        {
            // Получить orderProducts по order и включить Product
            var orderProducts = await _context.OrderProducts
                .Include(op => op.Product).ThenInclude(product => product.Manufacturer)
                .Include(op => op.Product).ThenInclude(product => product.UnitOfMeasure)
                .Include(op => op.Product).ThenInclude(product => product.Category)
                .Where(op => op.Order == order)
                .ToListAsync();

            var productModels = new ProductModel[orderProducts.Count];
            for (var i = 0; i < orderProducts.Count; i++)
            {
                var orderProduct = orderProducts[i];
                productModels[i] = new ProductModel(
                    orderProduct.Product.Id,
                    orderProduct.Product.Name,
                    orderProduct.Product.Description,
                    orderProduct.Product.ArticleCode,
                    orderProduct.Product.AdditionalNumber,
                    orderProduct.Product.Manufacturer.Id,
                    orderProduct.Product.UnitOfMeasure.Id,
                    orderProduct.Product.Category.Id,
                    orderProduct.Quantity
                );
            }

            orderModels.Add(new OrderModel(order.Id, order.Client.Id, order.Employee.Id, order.OrderStatus.Name, productModels));
        }

        return orderModels;
    }

    public async Task<bool> UpdateStatus(int id, string status)
    {
        // Получить order по id
        var order = await _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.OrderStatus)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);

        if (order == null) return false;

        // Получить статус заказа
        // Если статус заказа не существует, создать его и добавить в базу данных
        var orderStatus = await _context.OrderStatusEnumerable.FirstOrDefaultAsync(x => x.Name == status);
        if (orderStatus == null)
        {
            orderStatus = new OrderStatus { Name = status };
            await _context.OrderStatusEnumerable.AddAsync(orderStatus);
        }

        // Обновить статус заказа
        order.OrderStatus = orderStatus;
        await _context.SaveChangesAsync();
        return true;
    }
}