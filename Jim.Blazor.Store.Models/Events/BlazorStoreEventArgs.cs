using Jim.Blazor.Store.Models.Options;
using Jim.Core.Store.Models.Events;

namespace Jim.Blazor.Store.Models.Events
{
    public record BlazorStoreEventArgs : IBlazorStoreEventArgs
    {
        public BlazorStoreEventArgs()
        {
        }

        public BlazorStoreEventArgs(string key, StoreType storeType, JsStoreMethod method)
        {
            Key = key;
            StoreType = storeType;
            Method = method;
        }

        public StoreType StoreType { get; init; }

        public JsStoreMethod Method { get; init; }

        public string Key { get; init; }
    }
}
