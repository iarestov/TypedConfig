using System;
using ImpromptuInterface;
using TypedConfig.Deserialization;
using TypedConfig.TypedAdapter;

namespace PerformanceSmokeTest
{
    public class TypedConfig
    {
        public static T Create<T>(Func<string, string> flatConfigEntries, ITypeDeserializer typeDeserializer)
            where T : class
        {
            return
                new ValueCollectionToTypedClassAdapter(new TypedPropertyDeserializer<T>(flatConfigEntries,
                    typeDeserializer)).ActLike<T>();
        }
    }
}