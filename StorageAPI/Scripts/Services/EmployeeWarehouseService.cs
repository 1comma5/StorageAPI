using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class EmployeeWarehouseService
{
    private readonly StorageDbContext _context;
    public EmployeeWarehouseService(StorageDbContext context)
    {
        _context = context;
    }
    public async Task<EmployeeWarehouseModel?> Get(int id)
    {
        // Выбираем из базы данных объект EmployeeWarehouse по id и два id объектов, связанных с ним
        var employeeWarehouse = await _context.EmployeeWarehouses
            .Include(x => x.Warehouse)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        
        // Если объект не найден, возвращаем null
        if (employeeWarehouse == null) return null;
        
        return new EmployeeWarehouseModel(
            employeeWarehouse.Id,
            employeeWarehouse.Warehouse.Id,
            employeeWarehouse.Employee.Id
        );
    }

    public async Task<EmployeeWarehouseModel?> Add(EmployeeWarehouseModel employeeWarehouseModel)
    {
        // Выбираем из базы данных объекты Employee и Warehouse по id
        var employee = await _context.Employees.FirstOrDefaultAsync();
        var warehouse = await _context.Warehouses.FirstOrDefaultAsync();
        
        // Если хотя бы один из объектов не найден, возвращаем null
        if (employee == null || warehouse == null) return null;
        
        // Создаем новый объект EmployeeWarehouse и добавляем его в базу данных
        var employeeWarehouse = new EmployeeWarehouse
        {
            Warehouse = warehouse,
            Employee = employee
        };
        
        await _context.EmployeeWarehouses.AddAsync(employeeWarehouse);
        await _context.SaveChangesAsync();
        return new EmployeeWarehouseModel(
            employeeWarehouse.Id,
            employeeWarehouse.Warehouse.Id,
            employeeWarehouse.Employee.Id
        );
    }

    public async Task<EmployeeWarehouseModel?> Update(EmployeeWarehouseModel employeeWarehouseModel)
    {
        var employeeWarehouse = await _context.EmployeeWarehouses
            .Include(x => x.Warehouse)
            .Include(x => x.Employee)
            .FirstOrDefaultAsync(x => x.Id == employeeWarehouseModel.Id && !x.IsDeleted);
        if (employeeWarehouse == null) return null;
        
        var employee = await _context.Employees.FindAsync(employeeWarehouseModel.EmployeeId);
        var warehouse = await _context.Warehouses.FindAsync(employeeWarehouseModel.WarehouseId);
        
        if (employee != null) employeeWarehouse.Employee = employee;
        if (warehouse != null) employeeWarehouse.Warehouse = warehouse;
        
        _context.EmployeeWarehouses.Update(employeeWarehouse);
        await _context.SaveChangesAsync();
        
        return new EmployeeWarehouseModel(
            employeeWarehouse.Id,
            employeeWarehouse.Warehouse.Id,
            employeeWarehouse.Employee.Id
        );
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
    public async Task<List<EmployeeWarehouseModel>> GetAll()
    {
        var employeeWarehouses = await _context.EmployeeWarehouses
            .Include(x => x.Warehouse)
            .Include(x => x.Employee)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
        return employeeWarehouses.Select(x => new EmployeeWarehouseModel(
            x.Id,
            x.Warehouse.Id,
            x.Employee.Id
        )).ToList();
    }
  }