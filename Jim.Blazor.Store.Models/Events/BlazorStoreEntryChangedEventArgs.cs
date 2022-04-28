using Jim.Blazor.Store.Models.Options;

namespace Jim.Blazor.Store.Models.Events
{
    public record BlazorStoreEntryChangedEventArgs : BlazorStoreEventArgs
    {
        public BlazorStoreEntryChangedEventArgs()
        {
        }

        public BlazorStoreEntryChangedEventArgs(string key, StoreType storeType, JsStoreMethod method)
            : base(key, storeType, method)
        {
        }

        protected BlazorStoreEntryChangedEventArgs(BlazorStoreEventArgs original) : base(original)
        {
        }
    }
}
