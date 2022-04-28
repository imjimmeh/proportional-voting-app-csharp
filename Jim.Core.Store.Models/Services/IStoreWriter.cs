namespace Jim.Core.Store.Models.Services
{
    /// <summary>
    /// Service that manages writing to a store
    /// </summary>
    public interface IStoreWriter
    {
        /// <summary>
        /// Writes the given value to the store under the given key
        /// </summary>
        /// <typeparam name="T">Type of value</typeparam>
        /// <param name="key">Key ID</param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> WriteAsync<T>(string key, T value) where T : class;
    }
}