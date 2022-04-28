using Jim.Blazor.Store.Models;
using Microsoft.JSInterop;

namespace Jim.Blazor.Store.Services
{
    public abstract class StoreService
    {
        protected readonly IJSRuntime _js;
        protected readonly BlazorStoreOptions _options;

        public StoreService(BlazorStoreOptions options, IJSRuntime js)
        {
            _js = js ?? throw new ArgumentNullException(nameof(js));
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
    }
}
