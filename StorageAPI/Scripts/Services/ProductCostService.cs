using System.Security.Cryptography.X509Certificates;
using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class ProductCostService
{
   private readonly StorageDbContext _context;
   public ProductCostService(StorageDbContext context)
   {
       _context = context;
   }
   public async Task<ProductCostModel?> Get(int id)
   {
       var productCost = await _context.ProductCosts
           .Include(x => x.Product)
           .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
       if (productCost == null) return null;
       return new ProductCostModel(
           productCost.Id,
           productCost.Product.Id,
           productCost.Cost,
           productCost.ModificationDate
       );
   }

   public async Task<ProductCostModel?> Add(ProductCostModel productCostModel)
   {
       var product = await _context.Products.FindAsync(productCostModel.ProductId);
       if (product == null) return null;

       var productCost = new ProductCost
       {
           Product = product,
           Cost = productCostModel.Cost,
           ModificationDate = productCostModel.ModificationDate
       };

       await _context.ProductCosts.AddAsync(productCost);
       await _context.SaveChangesAsync();
       return new ProductCostModel(
           productCost.Id,
           productCost.Product.Id,
           productCost.Cost,
           productCost.ModificationDate);
   
}
   public async Task<ProductCostModel?> Update(ProductCost productCostModel)
   {
       var productCost = await _context.ProductCosts
           .Include(x => x.Product)
           .FirstOrDefaultAsync(x => x.Id == productCostModel.Id && !x.IsDeleted);
       if (productCost == null) return null;
       
       var product = await _context.Products.FindAsync(productCostModel.ProductId);
         if (product != null) productCost.Product = product;
         
         _context.ProductCosts.Update(productCost);
         await _context.SaveChangesAsync();
         return new ProductCostModel(
             productCost.Id,
             productCost.Product.Id,
             productCost.Cost,
             productCost.ModificationDate
         );
   }
   public async Task<bool> Delete(int id)
   {
       var product = await _context.ProductCosts.FindAsync(id);
       if (product == null) return false;
       product.IsDeleted = true;
       _context.ProductCosts.Update(product);
       await _context.SaveChangesAsync();
       return true;
   }
   public async Task<List<ProductCostModel>> GetAll()
   {
       var productCosts = await _context.ProductCosts
           .Include(x => x.Product)
           .Where(x => !x.IsDeleted)
           .ToListAsync();
       return productCosts.Select(x => new ProductCostModel(
           x.Id,
           x.Product.Id,
           x.Cost,
           x.ModificationDate
       )).ToList();
   }
}