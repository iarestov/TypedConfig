using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PersistedAttachedProperties.Persistance;
using TypedConfig.Deserialization;
using TypedConfig.TypedAdapter;

namespace PersistedAttachedProperties.AttachedProperties
{
    public class DbPersistedPropertyValueProvider<T> : IPropertyValueProvider 
        where T : class
    {
        private readonly Func<IAttachedPropertyContextLong> _contextCreator;
        private readonly T _defaultValues;
        private readonly long _ownerEntityId;
        private readonly Func<object, string> _serializer;
        private readonly TypedPropertyDeserializer<T> _typedPropertyDeserializer;

        private IDictionary<string, PersistedPropertyInfo> _knownProperties =
            new Dictionary<string, PersistedPropertyInfo>();

        private IDictionary<int, object> _loadedProperties = new Dictionary<int, object>();
        private IDictionary<string, string> _loadedSerializedProperties = new Dictionary<string, string>();

        public DbPersistedPropertyValueProvider(long ownerEntityId,
            Func<IAttachedPropertyContextLong> contextCreator,
            Func<object, string> serializer,
            T defaultValues,
            ITypeDeserializer typeDeserializer)
        {
            _defaultValues = defaultValues;
            _serializer = serializer;
            _contextCreator = contextCreator;
            _ownerEntityId = ownerEntityId;
            _typedPropertyDeserializer = new TypedPropertyDeserializer<T>(
                prop => _loadedSerializedProperties[prop],
                typeDeserializer
                );
        }

        public object GetValue(string propertyName)
        {
            PersistedPropertyInfo property;
            if (!_knownProperties.TryGetValue(propertyName, out property))
            {
                InitPropertyDescriptions();
                property = _knownProperties[propertyName];
            }

            object propertyValue;
            if (!_loadedProperties.TryGetValue(property.Id, out propertyValue))
            {
                LoadValues();
            }

            return _loadedProperties[property.Id];
        }

        private void LoadValues()
        {
            using (var context = _contextCreator.Invoke())
            {
                InitSerializedValues(context);
                _loadedProperties = _loadedSerializedProperties.ToDictionary(p => _knownProperties[p.Key].Id
                    , p => _typedPropertyDeserializer.GetValue(p.Key));
            }
        }

        private void InitSerializedValues(IAttachedPropertyContextLong context)
        {
            _loadedSerializedProperties = context.PropertyValues.Where(p => p.EntityId.CompareTo(_ownerEntityId) == 0)
                .ToDictionary(p => _knownProperties.Values.Single(v => v.Id == p.PropertyId).Info.Name,
                    p => p.Value);

            if (_defaultValues == null)
                return;

            foreach (var prop in typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => !_loadedSerializedProperties.ContainsKey(prop.Name)))
            {
                var propInfo = _knownProperties[prop.Name];

                var attachedPropertyValue = new AttachedPropertyValueLong
                {
                    EntityId = _ownerEntityId,
                    PropertyId = propInfo.Id,
                    Value = _serializer.Invoke(prop.GetValue(_defaultValues))
                };
                
                context.PropertyValues.Add(attachedPropertyValue);
                context.Save();

                _loadedSerializedProperties[prop.Name] = attachedPropertyValue.Value;
            }
        }

        private void InitPropertyDescriptions()
        {
            using (var context = _contextCreator.Invoke())
            {
                var type = typeof (T);
                var typeFullName = type.FullName;

                _knownProperties = context.Properties.Where(p => p.EntityType == typeFullName)
                    .ToDictionary(p => p.Name, p => new PersistedPropertyInfo
                    {
                        Id = p.Id,
                        Info = type.GetProperty(p.Name)
                    });

                var knowProperties = new Dictionary<string, PersistedPropertyInfo>();
                foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(prop => !_knownProperties.ContainsKey(prop.Name)))
                {
                    var p = context.Properties.Add(new AttachedProperty
                    {
                        EntityType = typeFullName,
                        Name = prop.Name,
                        Type = prop.PropertyType.FullName
                    });

                    knowProperties[prop.Name] = new PersistedPropertyInfo
                    {
                        Id = p.Id,
                        Info = prop
                    };
                }

                context.Save();

                foreach (var kv in knowProperties)
                {
                    _knownProperties[kv.Key] = kv.Value;
                }
            }
        }

        private class PersistedPropertyInfo
        {
            public int Id { get; set; }
            public PropertyInfo Info { get; set; }
        }
    }
}