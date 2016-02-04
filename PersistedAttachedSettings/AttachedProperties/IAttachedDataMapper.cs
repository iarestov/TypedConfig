using System;
using System.Collections.Generic;
using PersistedAttachedProperties.Persistance;
using TypedConfig.Deserialization;

namespace PersistedAttachedProperties.AttachedProperties
{
    public interface IAttachedDataMapper
    {
        void Register<T>(Func<object, string> sealizer, ITypeDeserializer desealizer) where T : class, IAttachedData;
        bool Unregister<T>() where T : class, IAttachedData;

        Dictionary<string, PersistedPropertyInfo> GetProperties<T>();
        ITypeDeserializer GetDeserializers<T>();
        Func<object, string> GetSerializers<T>();
    }


}
