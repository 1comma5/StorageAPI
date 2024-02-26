using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class ProductService
{
    private readonly StorageDbContext _context;
    
    public  ProductService(StorageDbContext context)
    {
        _context = context;
    }
    
    public async Task<ProductModel?> Get(int id)
    {
        var product = await _context.Products
            .Include(x => x.Manufacturer)
            .Include()
    
    public async Task<ProductModel?> Add(ProductModel productModel)
    {
        
    }
    public async Task<ProductModel?> Update(ProductModel productModel)
    {
        
    }
    public async Task<bool> Delete(int id)
    {
        
    }
    public async Task<List<ProductModel>> GetAll()
    {
        
    }
}