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
        var order = await _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.OrderStatus)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (order == null) return null;
        return new OrderModel(
            order.Id,
            order.Client.Id,
            order.Employee.Id,
            order.OrderStatus.Id
        );
    }

    public async Task<OrderModel?> Add(OrderModel orderModel)
    {
        // Выбираем из базы данных 
        var client = await _context.Clients.FindAsync(orderModel.ClientId);
        var employee = await _context.Employees.FindAsync(orderModel.EmployeeId);
        var orderStatus = await _context.OrderStatusEnumerable.FindAsync(orderModel.OrderStatusId);

        if (client == null || employee == null || orderStatus == null) return null;

        var order = new Order
        {
            Client = client,
            Employee = employee,
            OrderStatus = orderStatus
        };
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return new OrderModel(
            order.Id,
            order.Client.Id,
            order.Employee.Id,
            order.OrderStatus.Id
        );
    }

    public async Task<OrderModel?> Update(OrderModel orderModel)
    {
        var order = await _context.Orders
            .Include(x => x.Client)
            .Include(x => x.Employee)
            .Include(x => x.OrderStatus)
            .FirstOrDefaultAsync(x => x.Id == orderModel.Id && !x.IsDeleted);
        if (order == null) return null;
        var client = await _context.Clients.FindAsync(orderModel.ClientId);
        var employee = await _context.Employees.FindAsync(orderModel.EmployeeId);
        var orderStatus = await _context.OrderStatusEnumerable.FindAsync(orderModel.OrderStatusId);
        if (client != null) order.Client = client;
        if (employee != null) order.Employee = employee;
        if (orderStatus != null) order.OrderStatus = orderStatus;
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return new OrderModel(
            order.Id,
            order.Client.Id,
            order.Employee.Id,
            order.OrderStatus.Id
        );
    }

public async Task<bool> Delete(int id)
{
    var order = await _context.Orders.FindAsync(id);
    if (order == null) return false;
    order.IsDeleted = true;
    _context.Orders.Update(order);
    await _context.SaveChangesAsync();
    return true;
}
    public async Task<List<OrderModel>> GetAll()
    {
      var orders = await _context.Orders
          .Include(x => x.Client)
          .Include(x => x.Employee)
          .Include(x => x.OrderStatus)
          .Where(x => !x.IsDeleted)
          .ToListAsync();
      return orders.Select(x => new OrderModel(
        x.Id,
        x.Client.Id, 
        x.Employee.Id,
        x.OrderStatus.Id
      )).ToList();
    }
}