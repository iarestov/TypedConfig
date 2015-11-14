using System;

namespace TypedConfig
{
    public class SettingValueParseException : Exception
    {
        public SettingValueParseException(string settingName, string settingSerializedValue, Type propertyType, Exception exception)
        {
            

        }
    }
}