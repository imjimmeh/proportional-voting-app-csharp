using Jim.Blazor.Store.Models.Options;

namespace Jim.Blazor.Store.Models.Services
{
    public interface IStoreService
    {
        BlazorStoreOptions Options { get; }
    }
}