using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Microsoft.JSInterop;

namespace Jim.Blazor.Store.Services.Stores
{
    public abstract class StoreService : IBlazorStoreService
    {
        protected readonly IJSRuntime _js;
        protected readonly BlazorStoreOptions _options;

        protected readonly string methodPath;

        public StoreService(JsStoreMethod method, BlazorStoreOptions options, IJSRuntime js)
        {
            _js = js ?? throw new ArgumentNullException(nameof(js));
            _options = options ?? throw new ArgumentNullException(nameof(options));

            methodPath = _options.GetMethodPath(method);
        }

        public BlazorStoreOptions Options => _options;
    }
}
