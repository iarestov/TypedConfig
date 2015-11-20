using System;

namespace TypedConfig.Deserialization
{
    public class UnregisteredPropertyExcepiton : Exception
    {
        public UnregisteredPropertyExcepiton(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}