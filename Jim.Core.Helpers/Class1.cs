namespace Jim.Core.Helpers
{
    public class EnumConverter<TEnum, TValue> : IEnumConverter<TEnum, TValue>
    {
        private readonly Dictionary<TEnum, TValue> _mappings;

        private readonly Func<TEnum, bool> _isDefault;

        public EnumConverter(Dictionary<TEnum, TValue> mappings, Func<TEnum, bool> isDefault)
        {
            _mappings = mappings ?? throw new ArgumentNullException(nameof(mappings));
            _isDefault = isDefault ?? throw new ArgumentNullException(nameof(isDefault));
        }

        public virtual TValue ToValue(TEnum toConvert)
        {
            if (_isDefault.Invoke(toConvert))
                throw new ArgumentNullException(nameof(toConvert));

            if (_mappings.TryGetValue(toConvert, out TValue? type))
                return type ?? throw new Exception($"Found matching value for {toConvert} but is null");

            throw new Exception($"Could not find right mapping for {toConvert}");
        }

        public virtual TEnum ToEnum(string toConvert)
        {
            foreach (var type in _mappings)
            {
                if (Equals(type, toConvert))
                    return type.Key;
            }

            throw new Exception($"Could not find right mapping for {toConvert}");
        }
    }
}