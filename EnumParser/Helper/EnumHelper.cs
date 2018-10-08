namespace EnumParser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class EnumHelper : IEnumHelper
    {
        private Dictionary<ValueType, string> EnumValues;

        public EnumHelper(Type enumType, IEnumDescriptor enumDescriptor)
        {
            EnumType = enumType;
            EnumDescriptor = enumDescriptor;
            EnumValues = new Dictionary<ValueType, string>();
            InitializeDictionary();
        }

        public Type EnumType { get; }

        public IEnumDescriptor EnumDescriptor { get; }

        public string this[ValueType value] => EnumValues[value];

        public ValueType GetValue(string value) => (ValueType)Convert.ChangeType(Enum.Parse(EnumType, value), EnumType.GetEnumUnderlyingType());

        public string GetName(ValueType value) => Enum.Parse(EnumType, value.ToString()).ToString();

        public IEnumerator<KeyValuePair<ValueType, string>> GetEnumerator()
        {
            foreach (var kvp in EnumValues)
            {
                yield return kvp;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private void InitializeDictionary()
        {
            foreach (object obj in Enum.GetValues(EnumType))
            {
                string value = obj.ToString();
                EnumValues.Add(GetValue(value), value);
            }
        }
    }
}