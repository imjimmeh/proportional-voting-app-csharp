using Jim.Core.Helpers;

namespace Jim.Blazor.Store.Models.Options
{
    public static class StoreTypes
    {
        public const string LOCAL = "localStorage";
        public const string SESSION = "sessionStorage";

        private static Dictionary<StoreType, string>? _dictionary;

        private static IEnumConverter<StoreType, string> _converter
            = new EnumConverter<StoreType, string>(Stores,
                                                   storeType => storeType == StoreType.None);

        public static Dictionary<StoreType, string> Stores
        {
            get
            {
                if (_dictionary == null)
                    _dictionary = InitialiseDictionary();

                return _dictionary;
            }
        }
        public static string ToJSStoreName(this StoreType storeType) => _converter.ToValue(storeType);

        public static StoreType ToStoreType(this string storeType) => _converter.ToEnum(storeType);

        private static Dictionary<StoreType, string> InitialiseDictionary() => new Dictionary<StoreType, string> {
            { StoreType.Local, LOCAL }, { StoreType.Session, SESSION}
        };
    }
}
