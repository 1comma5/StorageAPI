using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;
public class OperationTypeService
{
    private readonly StorageDbContext _context;
    public OperationTypeService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<OperationType?> Get(int id)
    {
        return await _context.OperationTypes.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<OperationType?> Add(OperationType operationType)
    {
        operationType.IsDeleted = false;
        await _context.OperationTypes.AddAsync(operationType);
        await _context.SaveChangesAsync();
        return operationType;
    }
    public async Task<OperationType?> Update(OperationType operationType)
    {
        _context.OperationTypes.Update(operationType);
        await _context.SaveChangesAsync();
        return operationType;
    }
    public async Task<bool> Delete(int id)
    {
        var operationType = await _context.OperationTypes.FindAsync(id);
        if (operationType == null) return false;
        operationType.IsDeleted = true;
        _context.OperationTypes.Update(operationType);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<OperationType>> GetAll()
    {
        return await _context.OperationTypes.Where(x => !x.IsDeleted).ToListAsync();
    }
}