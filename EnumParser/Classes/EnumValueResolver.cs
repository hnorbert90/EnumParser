namespace EnumParser
{
    using System;

    public class EnumValueResolver : IEnumValueResolver
    {
        public const string s_HexadecimalPrefix = "0x";

        public EnumValueResolver()
        {
            Delimiter = ',';
        }

        public EnumValueResolver(char delimiter = ',')
        {
            Delimiter = delimiter;
        }

        public char Delimiter { get; set; }
    
        public virtual Tuple<string, ValueType> ResolveEnumValue(Type underlyingType, string rawValue)
        {
            if(rawValue == null || underlyingType == null)
            {
                throw new ArgumentNullException();
            }

            if (!ValueConverter.s_DecValueParserDictionary.ContainsKey(underlyingType) || !ValueConverter.s_HexValueParserDictionary.ContainsKey(underlyingType))
            {
                throw new NotSupportedException();
            }

            string[] enumValue = rawValue.Split(Delimiter);

            ValueType value;

            if (enumValue.Length == 2)
            {
                value = enumValue[1].StartsWith(s_HexadecimalPrefix)
                               ? ValueConverter.s_HexValueParserDictionary[underlyingType](enumValue[1].Substring(2))
                               : ValueConverter.s_DecValueParserDictionary[underlyingType](enumValue[1]);

                return new Tuple<string, ValueType>(enumValue[0], value);
            }

            value = enumValue[0].StartsWith(s_HexadecimalPrefix)
                                ? ValueConverter.s_HexValueParserDictionary[underlyingType](enumValue[0].Substring(2))
                                : ValueConverter.s_DecValueParserDictionary[underlyingType](enumValue[0]);

            return new Tuple<string, ValueType>(string.Empty, value);
        }
    }
}
