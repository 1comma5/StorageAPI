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
       var productHistory = await _context.ProductHistories
              .Include(p => p.OperationType)
              .Include(p => p.StorageLocationProduct)
              .Include(p => p.StorageLocation)
              .FirstOrDefaultAsync(p => p.Id == id);
        if (productHistory == null) return null;
        return new ProductHistoryModel(
            productHistory.Id,
            productHistory.Date,
            productHistory.OperationTypeId,
            productHistory.StorageLocationProductId,
            productHistory.Quantity,
            productHistory.StorageLocationId
        );
    }
    public async Task<ProductHistoryModel?> Add(ProductHistoryModel productHistoryModel)
    {
        var operationType = await _context.OperationTypes.FindAsync(productHistoryModel.OperationTypeId);
        var storageLocationProduct = await _context.StorageLocationProducts.FindAsync(productHistoryModel.StorageLocationProductId);
        var storageLocation = await _context.StorageLocations.FindAsync(productHistoryModel.StorageLocationId);
        if ( operationType == null || storageLocationProduct == null || storageLocation == null) return null;
        var productHistory = new ProductHistory
        {
            Date = productHistoryModel.Date,
            OperationType = operationType,
            StorageLocationProduct = storageLocationProduct,
            Quantity = productHistoryModel.Quantity,
            StorageLocation = storageLocation
        };
        await _context.ProductHistories.AddAsync(productHistory);
        await _context.SaveChangesAsync();
        return new ProductHistoryModel(
            productHistory.Id,
            productHistory.Date,
            productHistory.OperationTypeId,
            productHistory.StorageLocationProductId,
            productHistory.Quantity,
            productHistory.StorageLocationId
        );
        
    }
    public async Task<ProductHistoryModel?> Update(ProductHistoryModel productHistoryModel)
    {
        var productHistory = await _context.ProductHistories
            .Include(p => p.OperationType)
            .Include(p => p.StorageLocationProduct)
            .Include(p => p.StorageLocation)
            .FirstOrDefaultAsync(p => p.Id == productHistoryModel.Id);
        if (productHistory == null) return null;
        
        var operationType = await _context.OperationTypes.FindAsync(productHistoryModel.OperationTypeId);
        var storageLocationProduct = await _context.StorageLocationProducts.FindAsync(productHistoryModel.StorageLocationProductId);
        var storageLocation = await _context.StorageLocations.FindAsync(productHistoryModel.StorageLocationId);
        if (operationType != null) productHistory.OperationType = operationType;
        if (storageLocationProduct != null) productHistory.StorageLocationProduct = storageLocationProduct;
        if (storageLocation != null) productHistory.StorageLocation = storageLocation;
        
        _context.ProductHistories.Update(productHistory);
        await _context.SaveChangesAsync();
        return new ProductHistoryModel(
            productHistory.Id,
            productHistory.Date,
            productHistory.OperationTypeId,
            productHistory.StorageLocationProductId,
            productHistory.Quantity,
            productHistory.StorageLocationId
        );
        
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
    public async Task<List<ProductHistoryModel>> GetAll()
    {
        var productHistories = await _context.ProductHistories
            .Include(p => p.OperationType)
            .Include(p => p.StorageLocationProduct)
            .Include(p => p.StorageLocation)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
        return productHistories.Select(productHistory => new ProductHistoryModel(
            productHistory.Id,
            productHistory.Date,
            productHistory.OperationTypeId,
            productHistory.StorageLocationProductId,
            productHistory.Quantity,
            productHistory.StorageLocationId
        )).ToList();
    }
}