using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;

public class OrderService
{
    private readonly StorageDbContext _context;
    public OrderService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<Order?> Get(int id)
    {
        return await _context.Orders.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<Order?> Add(Order order)
    {
        order.IsDeleted = false;
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        return order;
    }
    public async Task<Order?> Update(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
        return order;
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
    public async Task<List<Order>> GetAll()
    {
        return await _context.Orders.Where(x => !x.IsDeleted).ToListAsync();
    }
}