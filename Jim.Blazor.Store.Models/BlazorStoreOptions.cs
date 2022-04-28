using Jim.Core.Store.Models.Options;

namespace Jim.Blazor.Store.Models
{
    public record BlazorStoreOptions : StoreOptions
    {
        public BlazorStoreOptions(StoreType storeType) : base(storeType.ToJSStoreName())
        {
        }

        protected BlazorStoreOptions(StoreType storeType, StoreOptions original) : base(original)
        {
        }

        public string GetMethodPath(string methodName) => StoreToUse + "." + methodName;
    }
}