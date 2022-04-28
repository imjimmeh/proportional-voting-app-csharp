using Jim.Blazor.Store.Models.Events;
using Jim.Blazor.Store.Models.Options;
using Jim.Blazor.Store.Models.Services;

namespace Jim.Blazor.Store.Services
{
    public class StoreWriterWatcher<TValue> : IBlazorStoreWriter, IBlazorStoreWriterWatcher<TValue?>
        where TValue : class?
    {
        private readonly IBlazorStoreWriter _writer;

        public StoreWriterWatcher(IBlazorStoreWriter writer)
        {
            _writer = writer;
        }

        public BlazorStoreOptions Options => _writer.Options;

        public event EventHandler<BlazorStoreEntryChangedEventArgs<TValue?>>? OnStoreWrite;

        public async Task<bool> WriteAsync(string key, TValue? value)
        {

            try
            {
                var result = await _writer.WriteAsync(key, value);

                if (result)
                    OnStoreWrite?.Invoke(this, GenerateEventArgs(key, value));

                return result;
            }
            catch
            {
                throw;
            }
        }
        public Task<bool> WriteAsync<T>(string key, T? value)
            where T : class?
            => value is TValue tValue ? WriteAsync(key, tValue) : throw new InvalidCastException($"{nameof(value)} is not expected type {typeof(TValue)}, but is {typeof(T)}");

        private BlazorStoreEntryChangedEventArgs<TValue?> GenerateEventArgs(string key, TValue? value)
            => new BlazorStoreEntryChangedEventArgs<TValue?>
            {
                Key = key,
                Method = JsStoreMethod.Set,
                StoreType = Options.StoreToUse.ToStoreType(),
                NewValue = value
            };
    }
}