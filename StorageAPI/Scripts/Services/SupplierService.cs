using StorageAPI.Scripts.Entities;
using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Models;

namespace StorageAPI.Scripts.Services;

public class SupplierService
{
    private readonly StorageDbContext _context;
    public SupplierService (StorageDbContext context)
    {
        _context = context;
    }

    public async Task<SupplierModel?> Get(int id)
    {
        var supplier = await _context.Suppliers
            .Include(x => x.ContactPerson)
            .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        if (supplier == null) return null;
        return new SupplierModel(
            supplier.Id,
            supplier.Name,
            supplier.Phone,
            supplier.Email,
            supplier.Address,
            supplier.ContactPerson.Id
        );
    }

    public async Task<SupplierModel?> Add(SupplierModel supplierModel)
    {
       var contactPerson = await _context.ContactPersons.FirstOrDefaultAsync();
        if (contactPerson == null) return null;
        var supplier = new Supplier
        {
            Name = supplierModel.Name,
            Phone = supplierModel.Phone,
            Email = supplierModel.Email,
            Address = supplierModel.Address,
            ContactPerson = contactPerson
        };
        await _context.Suppliers.AddAsync(supplier);
        await _context.SaveChangesAsync();
        return new SupplierModel(
            supplier.Id,
            supplier.Name,
            supplier.Phone,
            supplier.Email,
            supplier.Address,
            supplier.ContactPerson.Id
        );
    }
    public async Task<SupplierModel?> Update(SupplierModel supplierModel)
    {
      var supplier = await _context.Suppliers
            .Include(x => x.ContactPerson)
            .FirstOrDefaultAsync(x => x.Id == supplierModel.Id && !x.IsDeleted);
        if (supplier == null) return null;
        var contactPerson = await _context.ContactPersons.FindAsync(supplierModel.ContactPersonId);
        if (contactPerson != null) supplier.ContactPerson = contactPerson;
        _context.Suppliers.Update(supplier);
        await _context.SaveChangesAsync();
        
        return new SupplierModel(
            supplier.Id,
            supplier.Name,
            supplier.Phone,
            supplier.Email,
            supplier.Address,
            supplier.ContactPerson.Id
        );
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
    public async Task<List<SupplierModel>> GetAll()
    {
       var suppliers = await _context.Suppliers
            .Include(x => x.ContactPerson)
            .Where(x => !x.IsDeleted)
            .ToListAsync();
        return suppliers.Select(x => new SupplierModel(
            x.Id,
            x.Name,
            x.Phone,
            x.Email,
            x.Address,
            x.ContactPerson.Id
        )).ToList();
    }
    
}