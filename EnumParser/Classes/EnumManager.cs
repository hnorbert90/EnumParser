namespace EnumParser
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class EnumManager : IEnumManager
    {
        private static Dictionary<string, IEnumHelper> EnumDictionary;

        private static IEnumTypeGenerator _enumGenerator;

        static EnumManager()
        {
            EnumDictionary = new Dictionary<string, IEnumHelper>();
        }
        
        public EnumManager(IEnumTypeGenerator enumGenerator)
        {
            _enumGenerator = enumGenerator;
        }

        public IEnumHelper this[string index] => EnumDictionary[index];

        public IEnumHelper AddEnumType(IEnumDescriptor enumDescriptor)
        {
            if(enumDescriptor == null)
            {
                throw new ArgumentNullException();
            }

            if (!EnumDictionary.ContainsKey(enumDescriptor.EnumName))
            {
                Type enumType = _enumGenerator.CreateEnumType(enumDescriptor);

                EnumDictionary.Add(enumDescriptor.EnumName, new EnumHelper(enumType, enumDescriptor));
            }

            return EnumDictionary[enumDescriptor.EnumName];
        }

        public IEnumerator<KeyValuePair<string, IEnumHelper>> GetEnumerator()
        {
            foreach (var kvp in EnumDictionary)
            {
                yield return new KeyValuePair<string, IEnumHelper>(kvp.Key, kvp.Value);
            }
        }
       
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}