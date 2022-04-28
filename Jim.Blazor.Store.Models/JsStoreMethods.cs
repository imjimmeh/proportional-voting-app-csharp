using Jim.Core.Helpers;

namespace Jim.Blazor.Store.Models
{
    public static class JsStoreMethods
    {
        public const string GET_METHOD = "getItem";

        public const string SET_METHOD = "setItem";

        private static Dictionary<JsStoreMethod, string>? _dictionary;

        private static IEnumConverter<JsStoreMethod, string> _converter
            = new EnumConverter<JsStoreMethod, string>(Methods,
                                                   method => method == JsStoreMethod.None);

        public static Dictionary<JsStoreMethod, string> Methods
        {
            get
            {
                if (_dictionary == null)
                    _dictionary = InitialiseDictionary();

                return _dictionary;
            }
        }

        public static string ToMethodName(this JsStoreMethod jsStoreMethod) => _converter.ToValue(jsStoreMethod);

        public static JsStoreMethod ToJsStoreMethod(this string jsStoreMethod) => _converter.ToEnum(jsStoreMethod);

        private static Dictionary<JsStoreMethod, string> InitialiseDictionary() => new Dictionary<JsStoreMethod, string> {
            { JsStoreMethod.Get, GET_METHOD }, { JsStoreMethod.Set, SET_METHOD}
        };
    }
}
