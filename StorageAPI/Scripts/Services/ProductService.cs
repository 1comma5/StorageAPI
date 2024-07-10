using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class ProductService
{
    private readonly StorageDbContext _context;

    public ProductService(StorageDbContext context)
    {
        _context = context;
    }

    public async Task<ProductModel?> Get(int id)
    {
        var product = await _context.Products
            .Include(p => p.Manufacturer)
            .Include(p => p.UnitOfMeasure)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if (product == null) return null;
        return new ProductModel(
            product.Id,
            product.Name,
            product.Description,
            product.ArticleCode,
            product.AdditionalNumber,
            product.Manufacturer.Id,
            product.UnitOfMeasure.Id,
            product.Category.Id,
            product.Quantity
        );
    }

    public async Task<ProductModel?> Add(ProductModel productModel, int storageLocationId)
    {
        var manufacturer = await _context.Manufacturers.FirstOrDefaultAsync();
        if (manufacturer == null) return null;

        var unitOfMeasure = await _context.UnitOfMeasures.FirstOrDefaultAsync();
        if (unitOfMeasure == null) return null;

        var category = await _context.Categories.FirstOrDefaultAsync();
        if (category == null) return null;

        var storageLocation = await _context.StorageLocations.FindAsync(storageLocationId);
        if (storageLocation == null) return null;

        var supplier = await _context.Suppliers.FirstOrDefaultAsync();
        if (supplier == null) return null;

        var product = new Product
        {
            Name = productModel.Name,
            Description = productModel.Description,
            ArticleCode = productModel.ArticleCode,
            AdditionalNumber = productModel.AdditionalNumber,
            Manufacturer = manufacturer,
            UnitOfMeasure = unitOfMeasure,
            Category = category,
            Quantity = productModel.Quantity
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        var storageLocationProduct = new StorageLocationProduct
        {
            Product = product,
            StorageLocation = storageLocation,
            Quantity = productModel.Quantity,
            ArrivalDate = DateTime.UtcNow,
            ProductionDate = DateTime.UtcNow,
            ExpirationDate = DateTime.UtcNow.AddYears(1),
            Supplier = supplier,
            IsDeleted = false
        };

        await _context.StorageLocationProducts.AddAsync(storageLocationProduct);
        await _context.SaveChangesAsync();

        return new ProductModel(
            product.Id,
            product.Name,
            product.Description,
            product.ArticleCode,
            product.AdditionalNumber,
            product.Manufacturer.Id,
            product.UnitOfMeasure.Id,
            product.Category.Id,
            product.Quantity
        );
 }

public async Task<ProductModel?> Update(ProductModel productModel)
    {
        var product = await _context.Products
            .Include(p => p.Manufacturer)
            .Include(p => p.UnitOfMeasure)
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == productModel.Id);
        if (product == null) return null;

        var manufacturer = await _context.Manufacturers.FindAsync(productModel.ManufacturerId);
        var unitOfMeasure = await _context.UnitOfMeasures.FindAsync(productModel.UnitOfMeasureId);
        var category = await _context.Categories.FindAsync(productModel.CategoryId);

        if (manufacturer != null) product.Manufacturer = manufacturer;
        if (unitOfMeasure != null) product.UnitOfMeasure = unitOfMeasure;
        if (category != null) product.Category = category;

        _context.Products.Update(product);
        await _context.SaveChangesAsync();

        return new ProductModel(
            product.Id,
            product.Name,
            product.Description,
            product.ArticleCode,
            product.AdditionalNumber,
            product.Manufacturer.Id,
            product.UnitOfMeasure.Id,
            product.Category.Id,
            product.Quantity
        );
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

    public async Task<List<ProductModel>> GetAll()
    {
        var products = await _context.Products
            .Include(p => p.Manufacturer)
            .Include(p => p.UnitOfMeasure)
            .Include(p => p.Category)
            .Where(p => !p.IsDeleted)
            .ToListAsync();
        return products.Select(x => new ProductModel(
            x.Id,
            x.Name,
            x.Description,
            x.ArticleCode,
            x.AdditionalNumber,
            x.Manufacturer.Id,
            x.UnitOfMeasure.Id,
            x.Category.Id,
            x.Quantity
        )).ToList();
    }

    internal async Task<List<ProductModel>> GetAllStorageLocation(int storageLocationId)
    {
        var storageLocationProducts = await _context.StorageLocationProducts
            .Include(p => p.StorageLocation)
            .Include(p => p.Product)
                .ThenInclude(p => p.Manufacturer)
            .Include(p => p.Product)
                .ThenInclude(p => p.UnitOfMeasure)
            .Include(p => p.Product)
                .ThenInclude(p => p.Category)
            .Where(p => !p.IsDeleted && p.StorageLocationId == storageLocationId)
            .ToListAsync();

        return storageLocationProducts.Select(x => new ProductModel(
            x.Product.Id,
            x.Product.Name,
            x.Product.Description,
            x.Product.ArticleCode,
            x.Product.AdditionalNumber,
            x.Product.Manufacturer.Id,
            x.Product.UnitOfMeasure.Id,
            x.Product.Category.Id,
            x.Quantity
        )).ToList();
    }

}
