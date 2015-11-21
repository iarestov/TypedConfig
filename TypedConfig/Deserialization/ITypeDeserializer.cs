using System;

namespace TypedConfig.Deserialization
{
    public interface ITypeDeserializer
    {
        object Deserialize(Type type, string value);
        bool CanDeserialize(Type type);
    }
}