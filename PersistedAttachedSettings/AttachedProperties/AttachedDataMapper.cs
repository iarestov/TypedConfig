using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PersistedAttachedProperties.Persistance;
using TypedConfig.Deserialization;

namespace PersistedAttachedProperties.AttachedProperties
{
    public class AttachedDataMapper : IAttachedDataMapper
    {
        private static readonly Dictionary<Type, Dictionary<string, PersistedPropertyInfo>> KnownProperties =
            new Dictionary<Type, Dictionary<string, PersistedPropertyInfo>>();

        private static readonly Dictionary<Type, Func<object, string>> KnownSerializers =
            new Dictionary<Type, Func<object, string>>();

        private static readonly Dictionary<Type, ITypeDeserializer> KnownDeserializers =
            new Dictionary<Type, ITypeDeserializer>();

        private readonly IAttachedPropertyContextLong _ctx;

        private readonly object _guard = new object();

        public AttachedDataMapper(IAttachedPropertyContextLong ctx)
        {
            _ctx = ctx;
        }

        public void Register<T>(Func<object, string> serializer, ITypeDeserializer desealizer)
            where T : class, IAttachedData
        {
            var type = typeof(T);

            // ReSharper disable once InconsistentlySynchronizedField
            if (!KnownSerializers.ContainsKey(type))
            {
                lock (_guard)
                {
                    if (!KnownSerializers.ContainsKey(type))
                    {
                        KnownProperties.Add(type, InitDbPropertyDescriptions(type));
                        KnownDeserializers[type] = desealizer;
                        KnownSerializers[type] = serializer;
                    }
                }
            }

            //throw new ArgumentException(String.Format("Type {0} already registered", type));
        }

        public bool Unregister<T>() where T : class, IAttachedData
        {
            var type = typeof (T);

            // ReSharper disable once InconsistentlySynchronizedField
            if (KnownSerializers.ContainsKey(type))
            {
                lock (_guard)
                {
                    if (KnownSerializers.ContainsKey(type))
                    {
                        var hasInProps = KnownProperties.Remove(type);
                        var hasInDeserializers = KnownDeserializers.Remove(type);
                        var hasInSerializers = KnownSerializers.Remove(type);
                        return hasInProps || hasInSerializers || hasInDeserializers;
                    }
                }
            }

            return false;
        }

        public Dictionary<string, PersistedPropertyInfo> GetProperties<T>()
        {
            return GetByKey<T, Dictionary<string, PersistedPropertyInfo>>(KnownProperties);
        }

        public Func<object, string> GetSerializers<T>()
        {
            return GetByKey<T, Func<object, string>>(KnownSerializers);
        }

        public ITypeDeserializer GetDeserializers<T>()
        {
            return GetByKey<T, ITypeDeserializer>(KnownDeserializers);
        }

        private TResult GetByKey<TKey, TResult>(IDictionary<Type, TResult> collection)
        {
            var type = typeof(TKey);
            TResult result;
            if (!collection.TryGetValue(type, out result))
                throw new ArgumentException(type + "not registered");
            return result;
        }

        private Dictionary<string, PersistedPropertyInfo> InitDbPropertyDescriptions(Type type)
        {
            var typeFullName = type.FullName;

            var knownProperties = _ctx.Properties.Where(p => p.EntityType == typeFullName)
                .ToDictionary(p => p.Name, p => new PersistedPropertyInfo
                {
                    Id = p.Id,
                    Info = type.GetProperty(p.Name)
                });

            foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => !knownProperties.ContainsKey(prop.Name)))
            {
                if (prop.Name.Equals(Constants.Mapping.AttachedDataIdentityProperyName)) continue;

                _ctx.Properties.Add(new AttachedProperty
                {
                    EntityType = typeFullName,
                    Name = prop.Name,
                    Type = prop.PropertyType.FullName
                });

                knownProperties[prop.Name] = new PersistedPropertyInfo
                {
                    Info = prop
                };
            }

            _ctx.Save();

            foreach (var prop in _ctx.Properties.Where(x => x.EntityType == typeFullName).Select(x => new {x.Name, x.Id}).ToArray())
            {
                PersistedPropertyInfo ppi;
                if (knownProperties.TryGetValue(prop.Name, out ppi))
                {
                    ppi.Id = prop.Id;
                }
            }

            return knownProperties;
        }
    }
}
