using System;
using System.Data.Entity;

namespace TypedConfig.ExternalSettings
{
    public class AttachedPropsSet
    {
        public static T Create<T,U>(int entityId, Func<DbContext> dbContextFactory) where T:class where U:class
        {
            return null;
            // return new ValueCollectionToTypedClassAdapter(new TypedPropertyDeserializer<T>());
        }
    }
}