namespace Jim.Core.Store.Models.Events
{
    public interface IStoreEventArgs<TValue> : IStoreEventArgs
        where TValue : class?
    {
        TValue? NewValue { get; }
    }
    public interface IStoreEventArgs
    {
        string Key { get; }
    }
}
