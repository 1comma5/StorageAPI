using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;
public class OrderProductService
{
    private readonly StorageDbContext _context;
    public OrderProductService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<OrderProduct?> Get(int id)
    {
        return await _context.OrderProducts.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<OrderProduct?> Add(OrderProduct orderProduct)
    {
        orderProduct.IsDeleted = false;
        await _context.OrderProducts.AddAsync(orderProduct);
        await _context.SaveChangesAsync();
        return orderProduct;
    }
    public async Task<OrderProduct?> Update(OrderProduct orderProduct)
    {
        _context.OrderProducts.Update(orderProduct);
        await _context.SaveChangesAsync();
        return orderProduct;
    }
    public async Task<bool> Delete(int id)
    {
        var orderProduct = await _context.OrderProducts.FindAsync(id);
        if (orderProduct == null) return false;
        orderProduct.IsDeleted = true;
        _context.OrderProducts.Update(orderProduct);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<OrderProduct>> GetAll()
    {
        return await _context.OrderProducts.Where(x => !x.IsDeleted).ToListAsync();
    }
}