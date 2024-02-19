using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;

namespace StorageAPI.Scripts.Services;

public class EmployeeService
{
    private readonly StorageDbContext _context;

    public EmployeeService(StorageDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> Get(string id)
    {
        return await _context.Employees.FindAsync(id);
    }
    
    public async Task<Employee?> Add(Employee employee)
    {
        employee.IsDeleted = false;
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
    
    public async Task<Employee?> Update(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return employee;
    }
    
    public async Task<bool> Delete(string id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return false;
        employee.IsDeleted = true;
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Employee>> GetAll()
    {
        return await _context.Employees.ToListAsync();
    }
}