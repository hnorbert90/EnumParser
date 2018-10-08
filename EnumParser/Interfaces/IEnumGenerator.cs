namespace EnumParser
{
    using System;

    public interface IEnumTypeGenerator
    {
        Type CreateEnumType(IEnumDescriptor enumDescriptor);
    }
}