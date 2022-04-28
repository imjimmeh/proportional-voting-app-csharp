using Jim.Core.Store.Models.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Store.Models.Services
{
    /// <summary>
    /// Service that reads items from a store
    /// </summary>
    public interface IStoreReader<TOptions> : IStoreService<TOptions>
        where TOptions : StoreOptions
    {
        /// <summary>
        /// Get the object matching the given key
        /// </summary>
        /// <typeparam name="T">Expected type</typeparam>
        /// <param name="key">Key ID</param>
        /// <returns>Found value for key</returns>
        public Task<T?> GetAsync<T>(string key) where T : class;
    }
}
