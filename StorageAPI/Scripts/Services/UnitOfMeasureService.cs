using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;


public class UnitOfMeasureService
{
    private readonly StorageDbContext _context;
    public UnitOfMeasureService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<UnitOfMeasure?> Get(int id)
    {
        return await _context.UnitOfMeasures.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<UnitOfMeasure?> Add(UnitOfMeasure unitOfMeasure)
    {
        unitOfMeasure.IsDeleted = false;
        await _context.UnitOfMeasures.AddAsync(unitOfMeasure);
        await _context.SaveChangesAsync();
        return unitOfMeasure;
    }
    public async Task<UnitOfMeasure?> Update(UnitOfMeasure unitOfMeasure)
    {
        _context.UnitOfMeasures.Update(unitOfMeasure);
        await _context.SaveChangesAsync();
        return unitOfMeasure;
    }
    public async Task<bool> Delete(int id)
    {
        var unitOfMeasure = await _context.UnitOfMeasures.FindAsync(id);
        if (unitOfMeasure == null) return false;
        unitOfMeasure.IsDeleted = true;
        _context.UnitOfMeasures.Update(unitOfMeasure);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<UnitOfMeasure>> GetAll()
    {
        return await _context.UnitOfMeasures.Where(x => !x.IsDeleted).ToListAsync();
    }
    
}