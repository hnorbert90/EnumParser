namespace EnumParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumHelperExtensions
    {
        public static KeyValuePair<ValueType, string> ParseEnum(this IEnumHelper enumHelper, string rawEnumValue)
        {
            var enumValue = new EnumValueResolver(',').ResolveEnumValue(enumHelper.EnumDescriptor.DataType, rawEnumValue);

            return new KeyValuePair<ValueType, string>(enumValue.Item2, enumValue.Item1);
        }

        public static string GetRawEnumData(this IEnumHelper enumHelper, ValueType value) 
            => enumHelper.EnumDescriptor.Enumerations.First(rawEnum => rawEnum.StartsWith(enumHelper.GetName(value)));

        public static string GetDisplayValue(this IEnumHelper enumHelper, ValueType value) => $"{value} ({enumHelper.GetName(value)})";
    }
}