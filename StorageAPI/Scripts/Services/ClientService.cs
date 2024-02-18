using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;
public class ClientService
{
    private readonly StorageDbContext _context;

    public ClientService(StorageDbContext context)
    {
        _context = context;
    }

    public async Task<Client?> Get(string id)
    {
        return await _context.Clients.FindAsync(id);
    }
    
    public async Task<Client?> Add(Client client)
    {
        client.IsDeleted = false;
        await _context.Clients.AddAsync(client);
        await _context.SaveChangesAsync();
        return client;
    }
    
    public async Task<Client?> Update(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }
    
    public async Task<bool> Delete(string id)
    {
        var client = await _context.Clients.FindAsync(id);
        if (client == null) return false;
        client.IsDeleted = true;
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Client>> GetAll()
    {
        return await _context.Clients.ToListAsync();
    }
}
