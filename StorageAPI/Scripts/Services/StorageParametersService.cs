using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;


public class StorageParametersService
{
    private readonly StorageDbContext _context;
    public StorageParametersService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<StorageParameters?> Get(int id)
    {
        return await _context.StorageParametersEnumerable.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<StorageParameters?> Add(StorageParameters storageParameters)
    {
        storageParameters.IsDeleted = false;
        await _context.StorageParametersEnumerable.AddAsync(storageParameters);
        await _context.SaveChangesAsync();
        return storageParameters;
    }
    public async Task<StorageParameters?> Update(StorageParameters storageParameters)
    {
        _context.StorageParametersEnumerable.Update(storageParameters);
        await _context.SaveChangesAsync();
        return storageParameters;
    }
    public async Task<bool> Delete(int id)
    {
        var storageParameters = await _context.StorageParametersEnumerable.FindAsync(id);
        if (storageParameters == null) return false;
        storageParameters.IsDeleted = true;
        _context.StorageParametersEnumerable.Update(storageParameters);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<StorageParameters>> GetAll()
    {
        return await _context.StorageParametersEnumerable.Where(x => !x.IsDeleted).ToListAsync();
    }
    
}