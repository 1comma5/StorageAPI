using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;

public class EmployeeWarehouseService
{
    private readonly StorageDbContext _context;
    public EmployeeWarehouseService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<EmployeeWarehouse?> Get(int id)
    {
        return await _context.EmployeeWarehouses.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    public async Task<EmployeeWarehouse?> Add(EmployeeWarehouse employeeWarehouse)
    {
        employeeWarehouse.IsDeleted = false;
        await _context.EmployeeWarehouses.AddAsync(employeeWarehouse);
        await _context.SaveChangesAsync();
        return employeeWarehouse;
    }
    public async Task<EmployeeWarehouse?> Update(EmployeeWarehouse employeeWarehouse)
    {
        _context.EmployeeWarehouses.Update(employeeWarehouse);
        await _context.SaveChangesAsync();
        return employeeWarehouse;
    }
    public async Task<bool> Delete(int id)
    {
        var employeeWarehouse = await _context.EmployeeWarehouses.FindAsync(id);
        if (employeeWarehouse == null) return false;
        employeeWarehouse.IsDeleted = true;
        _context.EmployeeWarehouses.Update(employeeWarehouse);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<List<EmployeeWarehouse>> GetAll()
    {
        return await _context.EmployeeWarehouses.Where(x => !x.IsDeleted).ToListAsync();
    }
  }