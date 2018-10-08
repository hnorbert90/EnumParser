namespace EnumParser
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Reflection.Emit;

    public class EnumTypeGenerator : IEnumTypeGenerator
    {
        private const string dllExtension = ".dll";

        private static AppDomain currentDomain;
        private static AssemblyName assemblyName;
        private static AssemblyBuilder assemblyBuilder;
        private static ModuleBuilder moduleBuilder;
        private static CustomAttributeBuilder customAttributeBuilder;

        static EnumTypeGenerator() { 
            currentDomain = AppDomain.CurrentDomain;
            assemblyName = new AssemblyName(nameof(EnumTypeGenerator));
            assemblyBuilder = currentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);
            moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + dllExtension);
            customAttributeBuilder = new CustomAttributeBuilder(typeof(FlagsAttribute).GetConstructor(new Type[0]), new object[0]);
        }

        public EnumTypeGenerator(IEnumValueResolver enumValueResolver)
        {
            EnumValueResolver = enumValueResolver;
        }
        
        public IEnumValueResolver EnumValueResolver { get; set; }

        public Type CreateEnumType(IEnumDescriptor enumDescriptor)
        {
            EnumBuilder enumBuilder = moduleBuilder.DefineEnum(enumDescriptor.EnumName, TypeAttributes.Public, enumDescriptor.DataType);

            if (enumDescriptor.IsFlag)
            {
                enumBuilder.SetCustomAttribute(customAttributeBuilder);
            }

            foreach (var rawEnumValue in enumDescriptor.Enumerations)
            {
                Tuple<string, ValueType> enumValue = EnumValueResolver.ResolveEnumValue(enumDescriptor.DataType, rawEnumValue);

                enumBuilder.DefineLiteral(enumValue.Item1, enumValue.Item2);
            }

            return enumBuilder.CreateType();
        }
    }
}