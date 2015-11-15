using System;

namespace TypedConfig
{
    public class PropertyCannotBeFoundException : Exception
    {
        public PropertyCannotBeFoundException(string name, Exception exception)
        {
            
        }
    }
}