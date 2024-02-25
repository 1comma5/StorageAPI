using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;

public class ProductService
{
    private readonly StorageDbContext _context;
    
    public  ProductService(StorageDbContext context)
    {
        _context = context;
    }
    
    public async Task<Product?> Get(int id)
    {
        return await _context.Products.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<Product?> Add(Product product)
    {
        product.IsDeleted = false;
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<Product?> Update(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }
    public async Task<bool> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;
        product.IsDeleted = true;
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<Product>> GetAll()
    {
        return await _context.Products.Where(x => !x.IsDeleted).ToListAsync();
    }
}