using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class ProductHistoryService
{
    private readonly StorageDbContext _context;
    public ProductHistoryService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<ProductHistoryModel?> Get(int id)
    {
       
    }
    public async Task<ProductHistoryModel?> Add(ProductHistoryModel productHistoryModel)
    {
       
    }
    public async Task<ProductHistoryModel?> Update(ProductHistoryModel productHistoryModel)
    {
        
    }
    public async Task<bool> Delete(int id)
    {
        
    }
    public async Task<List<ProductHistoryModel>> GetAll()
    {
        
    }
}