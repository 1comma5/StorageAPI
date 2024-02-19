using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;

public class ManufacturerService
{
    private readonly StorageDbContext _context;

    public ManufacturerService(StorageDbContext context)
    {
        _context = context;
    }

    public async Task<Manufacturer?> Get(string id)
    {
        return await _context.Manufacturers.FindAsync(id);
    }
    
    public async Task<Manufacturer?> Add(Manufacturer manufacturer)
    {
        manufacturer.IsDeleted = false;
        await _context.Manufacturers.AddAsync(manufacturer);
        await _context.SaveChangesAsync();
        return manufacturer;
    }
    
    public async Task<Manufacturer?> Update(Manufacturer manufacturer)
    {
        _context.Manufacturers.Update(manufacturer);
        await _context.SaveChangesAsync();
        return manufacturer;
    }
    
    public async Task<bool> Delete(string id)
    {
        var manufacturer = await _context.Manufacturers.FindAsync(id);
        if (manufacturer == null) return false;
        manufacturer.IsDeleted = true;
        _context.Manufacturers.Update(manufacturer);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Manufacturer>> GetAll()
    {
        return await _context.Manufacturers.ToListAsync();
    }
}
