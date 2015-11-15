using System;

namespace TypedConfig
{
    public class UnregisteredTypeDesirializerExcepiton : Exception
    {
        public UnregisteredTypeDesirializerExcepiton(string propertyName, Type propertyType)
        {
            throw new NotImplementedException();
        }
    }
}