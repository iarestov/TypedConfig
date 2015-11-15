using System;

namespace TypedConfig
{
    public class UnregisteredPropertyExcepiton : Exception
    {
        public UnregisteredPropertyExcepiton(string propertyName)
        {
            throw new NotImplementedException();
        }
    }
}