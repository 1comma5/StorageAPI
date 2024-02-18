using Microsoft.EntityFrameworkCore;
using StorageAPI.Scripts.Entities;

namespace StorageAPI.Scripts.Services;

public class CategoryService(StorageDbContext context)
{
    public async Task<Category?> Get(string id)
    {
        return await context.Categories.FindAsync(id);
    }
    
    public async Task<Category?> Add(Category category)
    {
        category.IsDeleted = false;
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        return category;
    }
    
    public async Task<Category?> Update(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return category;
    }
    
    public async Task<bool> Delete(string id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category == null) return false;
        category.IsDeleted = true;
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Category>> GetAll()
    {
        return await context.Categories.ToListAsync();
    }
}