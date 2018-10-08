namespace EnumParser
{
    using System;
    using System.Collections.Generic;

    public class EnumDescriptor :IEnumDescriptor
    {
        private readonly Type m_DataType;
        private readonly List<string> m_Enumerations;
        private readonly string m_EnumName;
        private readonly bool m_IsFlag;

        public EnumDescriptor(Type dataType, List<string> enumerations, string enumName, bool isFlag)
        {
            m_DataType = dataType;
            m_Enumerations = enumerations;
            m_EnumName = enumName;
            m_IsFlag = isFlag;
        }

        public Type DataType
        {
            get { return m_DataType; }
        }

        public List<string> Enumerations
        {
            get { return m_Enumerations; }
        }

        public string EnumName
        {
            get { return m_EnumName; }
        }

        public bool IsFlag
        {
            get { return m_IsFlag; }
        }
    }
}