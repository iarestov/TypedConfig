using System;
using System.Data.Entity;

namespace TypedConfig
{
    public class AttachedPropsSet
    {
        public static T Create<T,U>(int entityId, Func<DbContext> dbContextFactory) where T:class where U:class
        {
            return null;
            // return new FlatValuesToTypedClassAdapter(new TypedPropertyDeserializer<T>());
        }
    }
}