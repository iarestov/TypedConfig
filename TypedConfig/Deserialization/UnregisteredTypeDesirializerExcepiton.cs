using System;

namespace TypedConfig.Deserialization
{
    public class UnregisteredTypeDesirializerExcepiton : Exception
    {
        public UnregisteredTypeDesirializerExcepiton(string propertyName, Type propertyType)
        {
        }
    }
}