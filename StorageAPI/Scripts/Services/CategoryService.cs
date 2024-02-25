using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;
public class CategoryService
{
    private readonly StorageDbContext _context;

    public CategoryService(StorageDbContext context)
    {
        _context = context;
    }

    public async Task<Category?> Get(int id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
    }
    
    public async Task<Category?> Add(Category category)
    {
        category.IsDeleted = false;
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
        return category;
    }
    
    public async Task<Category?> Update(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return category;
    }
    
    public async Task<bool> Delete(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;
        category.IsDeleted = true;
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Category>> GetAll()
    {
        return await _context.Categories.Where(x => !x.IsDeleted).ToListAsync();
    }
}