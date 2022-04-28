using Jim.Blazor.Store.Models.Options;
using Jim.Core.Store.Models.Events;

namespace Jim.Blazor.Store.Models.Events
{
    public record BlazorStoreEntryChangedEventArgs : BlazorStoreEventArgs
    {
        public BlazorStoreEntryChangedEventArgs()
        {
        }

        public BlazorStoreEntryChangedEventArgs(string key, StoreType storeType, JsStoreMethod method)
            : base(storeType, method)
        {
            Key = key;
        }

        protected BlazorStoreEntryChangedEventArgs(string key, BlazorStoreEventArgs original) : base(original)
        {
            Key = key;   
        }

        public string? Key { get; init; }
    }
}
