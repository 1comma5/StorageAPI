using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;


public class WarehouseService
{
    private readonly StorageDbContext _context;
    public WarehouseService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<Warehouse?> Get(int id)
    {
        return await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<Warehouse?> Add(Warehouse warehouse)
    {
        warehouse.IsDeleted = false;
        await _context.Warehouses.AddAsync(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }
    public async Task<Warehouse?> Update(Warehouse warehouse)
    {
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
        return warehouse;
    }
    public async Task<bool> Delete(int id)
    {
        var warehouse = await _context.Warehouses.FindAsync(id);
        if (warehouse == null) return false;
        warehouse.IsDeleted = true;
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<Warehouse>> GetAll()
    {
        return await _context.Warehouses.Where(x => !x.IsDeleted).ToListAsync();
    }
    
}