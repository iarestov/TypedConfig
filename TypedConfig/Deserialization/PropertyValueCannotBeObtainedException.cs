using System;

namespace TypedConfig
{
    public class PropertyValueCannotBeObtainedException : Exception
    {
        public PropertyValueCannotBeObtainedException(string name, Exception exception)
        {
            
        }
    }
}