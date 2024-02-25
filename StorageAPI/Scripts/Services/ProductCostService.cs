using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;

public class ProductCostService
{
   private readonly StorageDbContext _context;
   public ProductCostService(StorageDbContext context)
   {
       _context = context;
   }
   public async Task<ProductCost?> Get(int id)
   {
       return await _context.ProductCosts.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
   }
   public async Task<ProductCost?> Add(ProductCost productCost)
   {
       productCost.IsDeleted = false;
       await _context.ProductCosts.AddAsync(productCost);
       await _context.SaveChangesAsync();
       return productCost;
   }
   public async Task<ProductCost?> Update(ProductCost productCost)
   {
       _context.ProductCosts.Update(productCost);
       await _context.SaveChangesAsync();
       return productCost;
   }
   public async Task<bool> Delete(int id)
   {
       var productCost = await _context.ProductCosts.FindAsync(id);
       if (productCost == null) return false;
       productCost.IsDeleted = true;
       _context.ProductCosts.Update(productCost);
       await _context.SaveChangesAsync();
       return true;
   }
   public async Task<List<ProductCost>> GetAll()
   {
       return await _context.ProductCosts.Where(x => !x.IsDeleted).ToListAsync();
   }
   
}