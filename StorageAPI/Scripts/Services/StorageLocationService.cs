using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;
public class StorageLocationService
{
    private readonly StorageDbContext _context;
    public StorageLocationService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<StorageLocationModel?> Get(int id)
    {
       var storageLocation = await _context.StorageLocations
            .Include(x => x.Warehouse)
            .Include(x => x.StorageParameters)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (storageLocation == null) return null;
        return new StorageLocationModel(
            storageLocation.Id,
            storageLocation.StorageParametersId,
            storageLocation.Name,
            storageLocation.Description,
            storageLocation.WarehouseId,
            storageLocation.Settings
        );
    }
    public async Task<StorageLocationModel?> Add(StorageLocationModel storageLocationModel)
    {
       var storageParameters = await _context.StorageParametersEnumerable.FindAsync(storageLocationModel.StorageParametersId);
        var warehouse = await _context.Warehouses.FindAsync(storageLocationModel.WarehouseId);
        if (storageParameters == null || warehouse == null) return null;
        var storageLocation = new StorageLocation
        {
            StorageParameters = storageParameters,
            Warehouse = warehouse,
            Name = storageLocationModel.Name,
            Description = storageLocationModel.Description,
            Settings = storageLocationModel.Settings
        };
        await _context.StorageLocations.AddAsync(storageLocation);
        await _context.SaveChangesAsync();
        return new StorageLocationModel(
            storageLocation.Id,
            storageLocation.StorageParametersId,
            storageLocation.Name,
            storageLocation.Description,
            storageLocation.WarehouseId,
            storageLocation.Settings
        );
    }

    public async Task<StorageLocationModel?> Update(StorageLocationModel storageLocationModel)
    {
        var storageLocation = await _context.StorageLocations
            .Include(x => x.Warehouse)
            .Include(x => x.StorageParameters)
            .FirstOrDefaultAsync(x => x.Id == storageLocationModel.Id && !x.IsDeleted);
        if (storageLocation == null) return null;
        storageLocation.Name = storageLocationModel.Name;
        storageLocation.Description = storageLocationModel.Description;
        storageLocation.Settings = storageLocationModel.Settings;
        await _context.SaveChangesAsync();
        return new StorageLocationModel(
            storageLocation.Id,
            storageLocation.StorageParametersId,
            storageLocation.Name,
            storageLocation.Description,
            storageLocation.WarehouseId,
            storageLocation.Settings
        );
    }

    public async Task<bool> Delete(int id)
    {
      var storageLocation = await _context.StorageLocations.FindAsync(id);
        if (storageLocation == null) return false;
        storageLocation.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<StorageLocationModel>> GetAll()
    {
        var storageLocations = await _context.StorageLocations
            .Include(x => x.Warehouse)
            .Include(x => x.StorageParameters)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
        return storageLocations.Select(x => new StorageLocationModel(
            x.Id,
            x.StorageParametersId,
            x.Name,
            x.Description,
            x.WarehouseId,
            x.Settings
        )).ToList();
    }

}