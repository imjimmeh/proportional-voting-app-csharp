namespace Jim.Core.Helpers.Enums
{
    public interface IEnumConverter<TEnum, TValue>
    {
        TEnum ToEnum(string toConvert);
        TValue ToValue(TEnum toConvert);
    }
}