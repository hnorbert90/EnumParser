namespace EnumParser
{
    using System;

    public interface IEnumValueResolver
    {
        Tuple<string, ValueType> ResolveEnumValue(Type underlyingType, string rawValue);
    }
}
