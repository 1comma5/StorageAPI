using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;

public class ProductHistoryService
{
    private readonly StorageDbContext _context;
    public ProductHistoryService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<ProductHistory?> Get(int id)
    {
        return await _context.ProductHistories.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<ProductHistory?> Add(ProductHistory productHistory)
    {
        productHistory.IsDeleted = false;
        await _context.ProductHistories.AddAsync(productHistory);
        await _context.SaveChangesAsync();
        return productHistory;
    }
    public async Task<ProductHistory?> Update(ProductHistory productHistory)
    {
        _context.ProductHistories.Update(productHistory);
        await _context.SaveChangesAsync();
        return productHistory;
    }
    public async Task<bool> Delete(int id)
    {
        var productHistory = await _context.ProductHistories.FindAsync(id);
        if (productHistory == null) return false;
        productHistory.IsDeleted = true;
        _context.ProductHistories.Update(productHistory);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<ProductHistory>> GetAll()
    {
        return await _context.ProductHistories.Where(x => !x.IsDeleted).ToListAsync();
    }
}