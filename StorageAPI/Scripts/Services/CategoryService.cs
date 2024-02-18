using StorageAPI.Scripts.Interface;

namespace StorageAPI.Scripts.Services;

public class CategoryService : IBaseService
{
    public Task<T> Get<T>(string id) where T : class
    {
        private readonly CategoryService _categoryService;
        throw new NotImplementedException();
    }

    public Task<T> Create<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Update<T>(T entity) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<T> Delete<T>(string id) where T : class
    {
        throw new NotImplementedException();
    }

    public Task<List<T>> GetAll<T>() where T : class
    {
        throw new NotImplementedException();
    }
}