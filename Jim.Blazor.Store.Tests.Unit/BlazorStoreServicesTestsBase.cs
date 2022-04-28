using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Services.Stores;
using Jim.Core.Store.Models.Services;

namespace Jim.Blazor.Store.Tests.Unit
{
    public class BlazorStoreServicesTestsBase : StoreServicesTestsBase<BlazorStoreOptions>
    {
        public BlazorStoreServicesTestsBase(BlazorStoreOptions options) : base(options)
        {
        }

        protected internal override IStoreReader<BlazorStoreOptions> CreateReader()
            => new StoreReader(Options, JS);

        protected internal override IStoreWriter<BlazorStoreOptions> CreateWriter()
            => new StoreWriter(Options, JS);
    }
}
