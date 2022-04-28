using Jim.Core.Store.Models.Options;

namespace Jim.Blazor.Store.Models.Options
{
    public record BlazorStoreOptions : StoreOptions
    {
        public BlazorStoreOptions(StoreType storeType) : base(storeType.ToJSStoreName())
        {
        }

        protected BlazorStoreOptions(StoreType storeType, StoreOptions original) : base(original)
        {
        }

        public string GetMethodPath(JsStoreMethod method) => StoreToUse + "." + method.ToMethodName();

        public string GetMethodPath(string methodName) => StoreToUse + "." + methodName;
    }
}