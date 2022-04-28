using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;
using Microsoft.JSInterop;

namespace Jim.Blazor.Store.Services.Stores
{
    public abstract class StoreService : IStoreService
    {
        protected readonly IJSRuntime _js;
        protected readonly BlazorStoreOptions _options;

        public StoreService(BlazorStoreOptions options, IJSRuntime js)
        {
            _js = js ?? throw new ArgumentNullException(nameof(js));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public BlazorStoreOptions Options => _options;
    }
}
