namespace EnumParser
{
    using System;
    using System.Collections.Generic;

    public interface IEnumHelper : IEnumerable<KeyValuePair<ValueType, string>>
    {
        Type EnumType { get; }

        IEnumDescriptor EnumDescriptor { get; }

        ValueType GetValue(string value);

        string GetName(ValueType value);

        string this[ValueType value] { get; }
    }
}