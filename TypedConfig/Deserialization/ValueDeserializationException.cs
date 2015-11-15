using System;

namespace TypedConfig
{
    public class ValueDeserializationException : Exception
    {
        public ValueDeserializationException(string settingName, string settingSerializedValue, Type propertyType, Exception exception)
        {
            

        }
    }
}