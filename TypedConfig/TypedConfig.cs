using System;
using ImpromptuInterface;
using TypedConfig.Deserialization;
using TypedConfig.TypedAdapter;

namespace TypedConfig
{
    public class TypedConfig
    {
        public static T Create<T>(Func<string, string> flatConfigEntries) where T:class
        {
            return new FlatValuesToTypedClassAdapter(new TypedPropertyDeserializer<T>(flatConfigEntries)).ActLike<T>();
        }
    }
}