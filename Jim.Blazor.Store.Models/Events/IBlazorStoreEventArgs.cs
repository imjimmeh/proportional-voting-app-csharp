using Jim.Core.Store.Models.Events;

namespace Jim.Blazor.Store.Models.Events
{
    public interface IBlazorStoreEventArgs : IStoreEventArgs
    {
    }

    public interface IBlazorStoreEventArgs<TValue> : IBlazorStoreEventArgs, IStoreEventArgs<TValue>
        where TValue : class?
    {

    }
}
