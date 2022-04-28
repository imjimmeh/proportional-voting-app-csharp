namespace Jim.Core.Helpers
{
    public interface IEnumConverter<TEnum, TValue>
    {
        TEnum ToEnum(string toConvert);
        TValue ToValue(TEnum toConvert);
    }
}