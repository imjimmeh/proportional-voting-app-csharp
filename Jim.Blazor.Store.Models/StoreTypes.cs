namespace Jim.Blazor.Store.Models
{
    public static class StoreTypes
    {
        private static Dictionary<StoreType, string> _storeTypes = new Dictionary<StoreType, string>
        {
            { StoreType.Local, "localStorage" }, {StoreType.Session, "sessionStorage"}
        };

        public static string ToJSStoreName(this StoreType storeType)
        {
            if (storeType == StoreType.None)
                throw new ArgumentNullException(nameof(storeType));

            if (_storeTypes.TryGetValue(storeType, out string? type))
                return type ?? throw new Exception($"Found matching value for {storeType} but is null");

            throw new Exception($"Could not find right mapping for {storeType}");
        }

        public static StoreType ToStoreType(this string storeType)
        {
            foreach(var type in _storeTypes)
            {
                if (Equals(type, storeType))
                    return type.Key;
            }

            throw new Exception($"Could not find right mapping for {storeType}");
        }

    }
}
