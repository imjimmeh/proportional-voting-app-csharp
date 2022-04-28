using Jim.Core.Store.Models.Events;
using Jim.Core.Store.Models.Options;
using Jim.Core.Store.Models.Services;

namespace Jim.Core.Store.Models.Watchers
{
    public interface IStoreWriterWatcher<TOptions, TStoreChangeArgs> : IStoreWriter<TOptions>
        where TOptions : StoreOptions
        where TStoreChangeArgs : IStoreEventArgs
    {
        /// <summary>
        /// Event that is fired when a key is written to
        /// </summary>
        event EventHandler<TStoreChangeArgs>? OnStoreWrite;
    }
}
