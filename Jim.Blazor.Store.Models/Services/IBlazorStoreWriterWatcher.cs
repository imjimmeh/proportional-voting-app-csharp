using Jim.Blazor.Store.Models.Events;
using Jim.Blazor.Store.Models.Options;
using Jim.Core.Store.Models.Watchers;

namespace Jim.Blazor.Store.Models.Services
{
    public interface IBlazorStoreWriterWatcher<TValue> : IStoreWriterWatcher<BlazorStoreOptions, BlazorStoreEntryChangedEventArgs<TValue?>, TValue>
        where TValue : class?
    {
    }
}