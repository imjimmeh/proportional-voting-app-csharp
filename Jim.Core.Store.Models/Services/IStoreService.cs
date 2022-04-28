using Jim.Core.Store.Models.Options;

namespace Jim.Core.Store.Models.Services
{
    /// <summary>
    /// Base service for store management
    /// </summary>
    public interface IStoreService<TOptions>
        where TOptions : StoreOptions
    {
        TOptions Options { get; }
    }
}