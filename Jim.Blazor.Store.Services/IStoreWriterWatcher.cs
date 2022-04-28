using Jim.Blazor.Store.Models.Events;
using Jim.Blazor.Store.Models.Services;

namespace Jim.Blazor.Store.Services
{
    public interface IStoreWriterWatcher : IBlazorStoreWriter
    {
        event EventHandler<BlazorStoreEntryChangedEventArgs>? OnStoreWrite;
    }
}