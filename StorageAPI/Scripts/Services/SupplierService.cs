using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;

public class SupplierService
{
    private readonly StorageDbContext _context;
    public SupplierService (StorageDbContext context)
    {
        _context = context;
    }
    public async Task<Supplier?> Get(int id)
    {
        return await _context.Suppliers.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<Supplier?> Add(Supplier supplier)
    {
        supplier.IsDeleted = false;
        await _context.Suppliers.AddAsync(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }
    public async Task<Supplier?> Update(Supplier supplier)
    {
        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
        return supplier;
    }
    public async Task<bool> Delete(int id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);
        if (supplier == null) return false;
        supplier.IsDeleted = true;
        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<Supplier>> GetAll()
    {
        return await _context.Suppliers.Where(x => !x.IsDeleted).ToListAsync();
    }
    
}