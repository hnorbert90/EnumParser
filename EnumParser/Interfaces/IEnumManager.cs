namespace EnumParser
{
    using System.Collections.Generic;

    public interface IEnumManager : IEnumerable<KeyValuePair<string, IEnumHelper>>
    {
        IEnumHelper AddEnumType(IEnumDescriptor enumDescriptor);
        IEnumHelper this[string index] { get; }
    }
}