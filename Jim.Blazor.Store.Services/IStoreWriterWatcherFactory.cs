using Jim.Blazor.Store.Models.Services;

namespace Jim.Blazor.Store.Services
{
    public interface IStoreWriterWatcherFactory
    {
        IBlazorStoreWriterWatcher<TValue?> GetWatcher<TValue>() where TValue : class?;
    }
}