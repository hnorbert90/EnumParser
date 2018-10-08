namespace EnumParser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public static class ValueConverter
    {
        static ValueConverter()
        {
            s_HexValueParserDictionary = new Dictionary<Type, Func<string, ValueType>>
            {
                {typeof(byte), value => byte.Parse(value, NumberStyles.AllowHexSpecifier)},
                {typeof(sbyte), value => sbyte.Parse(value, NumberStyles.AllowHexSpecifier)},
                {typeof(short), value => short.Parse(value, NumberStyles.AllowHexSpecifier)},
                {typeof(ushort), value => ushort.Parse(value, NumberStyles.AllowHexSpecifier)},
                {typeof(int), value => int.Parse(value, NumberStyles.AllowHexSpecifier)},
                {typeof(uint), value => uint.Parse(value, NumberStyles.AllowHexSpecifier)},
                {typeof(long), value => long.Parse(value, NumberStyles.AllowHexSpecifier)},
                {typeof(ulong), value => ulong.Parse(value, NumberStyles.AllowHexSpecifier)},
            };

            s_DecValueParserDictionary = new Dictionary<Type, Func<string, ValueType>>
            {
                {typeof(byte), value => byte.Parse(value)},
                {typeof(sbyte), value => sbyte.Parse(value)},
                {typeof(short), value => short.Parse(value)},
                {typeof(ushort), value => ushort.Parse(value)},
                {typeof(int), value => int.Parse(value)},
                {typeof(uint), value => uint.Parse(value)},
                {typeof(long), value => long.Parse(value)},
                {typeof(ulong), value => ulong.Parse(value)},
            };
        }

        public static Dictionary<Type, Func<string, ValueType>> s_HexValueParserDictionary { get; }

        public static Dictionary<Type, Func<string, ValueType>> s_DecValueParserDictionary { get; }
    }
}