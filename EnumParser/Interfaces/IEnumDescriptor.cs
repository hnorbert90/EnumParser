namespace EnumParser
{
    using System;
    using System.Collections.Generic;

    public interface IEnumDescriptor
    {
        string EnumName { get; }

        Type DataType { get; }

        List<string> Enumerations { get; }

        bool IsFlag { get; }
    }
}