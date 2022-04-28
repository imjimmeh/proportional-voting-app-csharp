using static Jim.Core.Extensions.StringExtensions;

namespace Jim.Core.Store.Models.Options
{
    public record StoreOptions
    {
        private string _storeToUse;

        public StoreOptions()
        {
        }

        public StoreOptions(string storeToUse)
        {
            _storeToUse = !string.IsNullOrEmpty(storeToUse) ? storeToUse : throw new ArgumentNullException(nameof(storeToUse));
        }

        public string StoreToUse => _storeToUse;
    }
}
