using System;

namespace TypedConfig.Deserialization
{
    public class PropertyValueCannotBeObtainedException : Exception
    {
        public PropertyValueCannotBeObtainedException(string name, Exception exception)
        {
        }
    }
}