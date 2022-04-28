using Jim.Core.Store.Models.Events;
using Jim.Core.Store.Models.Options;
using Jim.Core.Store.Models.Services;

namespace Jim.Core.Store.Models.Watchers
{
    /// <summary>
    /// Watches the given store, and fires an event everytime a value is changed
    /// </summary>
    /// <typeparam name="TOptions">Store service options</typeparam>
    /// <typeparam name="TStoreChangeArgs">EventArgs that should be fired when a key's value is changed</typeparam>
    /// <typeparam name="TValue">Type of value that will be changed</typeparam>
    public interface IStoreWriterWatcher<TOptions, TStoreChangeArgs, TValue> : IStoreWriter<TOptions>, IStoreWriterWatcher
        where TOptions : StoreOptions
        where TStoreChangeArgs : IStoreEventArgs<TValue?>
        where TValue : class?
    {
        /// <summary>
        /// Event that is fired when a key is written to
        /// </summary>
        event EventHandler<TStoreChangeArgs>? OnStoreWrite;
    }

    public interface IStoreWriterWatcher 
    { 
    }
}
