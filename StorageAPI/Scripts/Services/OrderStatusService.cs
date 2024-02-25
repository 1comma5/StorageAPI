using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;

public class OrderStatusService
{
    private readonly StorageDbContext _context;
    
    public OrderStatusService(StorageDbContext context)
    {
        _context = context;
    }
    
    public async Task<OrderStatus?> Get(int id)
    {
        return await _context.OrderStatusEnumerable.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    
    public async Task<OrderStatus?> Add(OrderStatus orderStatus)
    {
        orderStatus.IsDeleted = false;
        await _context.OrderStatusEnumerable.AddAsync(orderStatus);
        await _context.SaveChangesAsync();
        return orderStatus;
    }
    
    public async Task<OrderStatus?> Update(OrderStatus orderStatus)
    {
        _context.OrderStatusEnumerable.Update(orderStatus);
        await _context.SaveChangesAsync();
        return orderStatus;
    }
    
    public async Task<bool> Delete(int id)
    {
        var orderStatus = await _context.OrderStatusEnumerable.FindAsync(id);
        if (orderStatus == null) return false;
        orderStatus.IsDeleted = true;
        _context.OrderStatusEnumerable.Update(orderStatus);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<OrderStatus>> GetAll()
    {
        return await _context.OrderStatusEnumerable.Where(x => !x.IsDeleted).ToListAsync();
    }
}