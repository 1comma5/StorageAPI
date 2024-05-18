using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class StorageLocationProductService
{
    private readonly StorageDbContext _context;
    public StorageLocationProductService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<StorageLocationProductModel?> Get(int id)
    {
     var storageLocationProduct = await _context.StorageLocationProducts
            .Include(x => x.Product)
            .Include(x => x.StorageLocation)
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        
        if (storageLocationProduct == null) return null;
        
        return new StorageLocationProductModel(
            storageLocationProduct.Id,
            storageLocationProduct.Product.Id,
            storageLocationProduct.StorageLocation.Id,
            storageLocationProduct.Quantity,
            storageLocationProduct.ArrivalDate,
            storageLocationProduct.ProductionDate,
            storageLocationProduct.ExpirationDate,
            storageLocationProduct.Supplier.Id
        );
    }

    public async Task<StorageLocationProductModel?> Add(StorageLocationProductModel storageLocationProductModel)
    {
        var product = await _context.Products.FirstOrDefaultAsync();
        if (product == null) return null;
        var storageLocation = await _context.StorageLocations.FirstOrDefaultAsync();
        if (storageLocation == null) return null;
        var supplier = await _context.Suppliers.FirstOrDefaultAsync();
        if (supplier == null) return null;
        if (product == null || storageLocation == null || supplier == null) return null;
        var storageLocationProduct = new StorageLocationProduct
        {
            Product = product,
            StorageLocation = storageLocation,
            Supplier = supplier,
            Quantity = storageLocationProductModel.Quantity,
            ArrivalDate = storageLocationProductModel.ArrivalDate,
            ProductionDate = storageLocationProductModel.ProductionDate,
            ExpirationDate = storageLocationProductModel.ExpirationDate
        };
        await _context.StorageLocationProducts.AddAsync(storageLocationProduct);
        await _context.SaveChangesAsync();
        return new StorageLocationProductModel(
            storageLocationProduct.Id,
            storageLocationProduct.Product.Id,
            storageLocationProduct.StorageLocation.Id,
            storageLocationProduct.Quantity,
            storageLocationProduct.ArrivalDate,
            storageLocationProduct.ProductionDate,
            storageLocationProduct.ExpirationDate,
            storageLocationProduct.Supplier.Id
        );
    }

    public async Task<StorageLocationProductModel?> Update(StorageLocationProductModel storageLocationProductModel)
    {
        var storageLocationProduct = await _context.StorageLocationProducts
            .Include(x => x.Product)
            .Include(x => x.StorageLocation)
            .Include(x => x.Supplier)
            .FirstOrDefaultAsync(x => x.Id == storageLocationProductModel.Id && !x.IsDeleted);
        if (storageLocationProduct == null) return null;
        var product = await _context.Products.FindAsync(storageLocationProductModel.ProductId);
        var storageLocation = await _context.StorageLocations.FindAsync(storageLocationProductModel.StorageLocationId);
        var supplier = await _context.Suppliers.FindAsync(storageLocationProductModel.SupplierId);
       
        if (product != null) storageLocationProduct.Product = product;
        if (storageLocation != null) storageLocationProduct.StorageLocation = storageLocation;
        if (supplier != null) storageLocationProduct.Supplier = supplier;
        
        _context.StorageLocationProducts.Update(storageLocationProduct);
        await _context.SaveChangesAsync();
        return new StorageLocationProductModel(
            storageLocationProduct.Id,
            storageLocationProduct.Product.Id,
            storageLocationProduct.StorageLocation.Id,
            storageLocationProduct.Quantity,
            storageLocationProduct.ArrivalDate,
            storageLocationProduct.ProductionDate,
            storageLocationProduct.ExpirationDate,
            storageLocationProduct.Supplier.Id
        );
    }

    public async Task<bool> Delete(int id)
    {
        var storageLocationProduct = await _context.StorageLocationProducts.FindAsync();
           if (storageLocationProduct == null) return false;
           storageLocationProduct.IsDeleted = true;
           _context.StorageLocationProducts.Update(storageLocationProduct);
           await _context.SaveChangesAsync();
           return true;

    }

    public async Task<List<StorageLocationProductModel>> GetAll()
    {
        var storageLocationProducts = await _context.StorageLocationProducts
            .Include(x => x.Product)
            .Include(x => x.StorageLocation)
            .Include(x => x.Supplier)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
        return storageLocationProducts.Select
        (x => new StorageLocationProductModel(
            x.Id,
            x.Product.Id,
            x.StorageLocation.Id,
            x.Quantity,
            x.ArrivalDate,
            x.ProductionDate,
            x.ExpirationDate,
            x.Supplier.Id
        )).ToList();
}

}