using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;

public class ContactPersonService
{
    private readonly StorageDbContext _context;

    public ContactPersonService(StorageDbContext context)
    {
        _context = context;
    }

    public async Task<ContactPerson?> Get(int id)
    {
        return await _context.ContactPersons.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    
    public async Task<ContactPerson?> Add(ContactPerson contactPerson)
    {
        contactPerson.IsDeleted = false;
        await _context.ContactPersons.AddAsync(contactPerson);
        await _context.SaveChangesAsync();
        return contactPerson;
    }
    
    public async Task<ContactPerson?> Update(ContactPerson contactPerson)
    {
        _context.ContactPersons.Update(contactPerson);
        await _context.SaveChangesAsync();
        return contactPerson;
    }
    
    public async Task<bool> Delete(int id)
    {
        var contactPerson = await _context.ContactPersons.FindAsync(id);
        if (contactPerson == null) return false;
        contactPerson.IsDeleted = true;
        _context.ContactPersons.Update(contactPerson);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<ContactPerson>> GetAll()
    {
        return await _context.ContactPersons.Where(x => !x.IsDeleted).ToListAsync();
    }
}