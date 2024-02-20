using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;
public class StorageLocationService
{
    private readonly StorageDbContext _context;
    public StorageLocationService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<StorageLocation?> Get(string id)
    {
        return await _context.StorageLocations.FindAsync(id);
    }
    public async Task<StorageLocation?> Add(StorageLocation storageLocation)
    {
        storageLocation.IsDeleted = false;
        await _context.StorageLocations.AddAsync(storageLocation);
        await _context.SaveChangesAsync();
        return storageLocation;
    }
    public async Task<StorageLocation?> Update(StorageLocation storageLocation)
    {
        _context.StorageLocations.Update(storageLocation);
        await _context.SaveChangesAsync();
        return storageLocation;
    }
    public async Task<bool> Delete(string id)
    {
        var storageLocation = await _context.StorageLocations.FindAsync(id);
        if (storageLocation == null) return false;
        storageLocation.IsDeleted = true;
        _context.StorageLocations.Update(storageLocation);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<StorageLocation>> GetAll()
    {
        return await _context.StorageLocations.ToListAsync();
    }
    
}