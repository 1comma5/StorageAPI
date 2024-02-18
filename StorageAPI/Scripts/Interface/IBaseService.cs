namespace StorageAPI.Scripts.Interface;

public interface IBaseService
{
    public Task<T> Get<T>(string id) where T : class;
    public Task<T> Create<T>(T entity) where T : class;
    public Task<T> Update<T>(T entity) where T : class;
    public Task<T> Delete<T>(string id) where T : class;
    public Task<List<T>> GetAll<T>() where T : class;
}