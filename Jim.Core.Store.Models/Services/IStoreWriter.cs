namespace Jim.Core.Store.Models.Services
{
    public interface IStoreWriter
    {
        Task<bool> WriteAsync<T>(string key, T value) where T : class;
    }
}