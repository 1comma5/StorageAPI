using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;

public class StorageLocationProductService
{
    private readonly StorageDbContext _context;
    public StorageLocationProductService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<StorageLocationProduct?> Get(int id)
    {
        return await _context.StorageLocationProducts.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<StorageLocationProduct?> Add(StorageLocationProduct storageLocationProduct)
    {
        storageLocationProduct.IsDeleted = false;
        await _context.StorageLocationProducts.AddAsync(storageLocationProduct);
        await _context.SaveChangesAsync();
        return storageLocationProduct;
    }
    public async Task<StorageLocationProduct?> Update(StorageLocationProduct storageLocationProduct)
    {
        _context.StorageLocationProducts.Update(storageLocationProduct);
        await _context.SaveChangesAsync();
        return storageLocationProduct;
    }
    public async Task<bool> Delete(int id)
    {
        var storageLocationProduct = await _context.StorageLocationProducts.FindAsync(id);
        if (storageLocationProduct == null) return false;
        storageLocationProduct.IsDeleted = true;
        _context.StorageLocationProducts.Update(storageLocationProduct);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<StorageLocationProduct>> GetAll()
    {
        return await _context.StorageLocationProducts.Where(x => !x.IsDeleted).ToListAsync();
    }
    
}