using System;

namespace TypedConfig.Deserialization
{
    public class ValueDeserializationException : Exception
    {
        public ValueDeserializationException(string propertyName, string settingSerializedValue, Type propertyType,
            Exception exception)
        {
        }
    }
}