using System;

namespace TypedConfig.Deserialization
{
    public class ValueDeserializationException : Exception
    {
        public ValueDeserializationException(string settingName, string settingSerializedValue, Type propertyType, Exception exception)
        {
            

        }
    }
}