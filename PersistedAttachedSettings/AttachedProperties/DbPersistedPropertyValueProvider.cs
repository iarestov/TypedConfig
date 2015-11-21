using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TypedConfig.Deserialization;
using TypedConfig.TypedAdapter;

namespace PersistedAttachedProperties.AttachedProperties
{
    public class DbPersistedPropertyValueProvider<T> : IPropertyValueProvider where T : class
    {
        private readonly Func<IAttachedPropertyContext> _contextCreator;
        private readonly T _defaultValues;
        private readonly int _ownerEntityId;
        private readonly Func<object, string> _serializer;
        private readonly TypedPropertyDeserializer<T> _typedPropertyDeserializer;

        private IDictionary<string, PersistedPropertyInfo> _knownProperties =
            new Dictionary<string, PersistedPropertyInfo>();

        private IDictionary<int, object> _loadedProperties = new Dictionary<int, object>();
        private IDictionary<string, string> _loadedSerializedProperties = new Dictionary<string, string>();

        public DbPersistedPropertyValueProvider(int ownerEntityId,
            Func<IAttachedPropertyContext> contextCreator,
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

        private void InitSerializedValues(IAttachedPropertyContext context)
        {
            _loadedSerializedProperties = context.PropertyValues.Where(p => p.EntityId == _ownerEntityId)
                .ToDictionary(p => _knownProperties.Values.Single(v => v.Id == p.PropertyId).Info.Name,
                    p => p.Value);

            foreach (var prop in typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Where(prop => !_loadedSerializedProperties.ContainsKey(prop.Name)))
            {
                var propInfo = _knownProperties[prop.Name];

                var attachedPropertyValue = new AttachedPropertyValue
                {
                    EntityId = _ownerEntityId,
                    PropertyId = propInfo.Id,
                    Value = _serializer.Invoke(prop.GetValue(_defaultValues))
                };
                var p = context.PropertyValues.Add(attachedPropertyValue);
                context.Save();

                _loadedSerializedProperties[prop.Name] = attachedPropertyValue.Value;
            }
        }

        private void InitPropertyDescriptions()
        {
            using (var context = _contextCreator.Invoke())
            {
                _knownProperties = context.Properties.Where(p => p.EntityType == typeof (T).FullName)
                    .ToDictionary(p => p.Name, p => new PersistedPropertyInfo
                    {
                        Id = p.Id,
                        Info = typeof (T).GetProperty(p.Name)
                    });

                foreach (var prop in typeof (T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .Where(prop => !_knownProperties.ContainsKey(prop.Name)))
                {
                    var p = context.Properties.Add(new AttachedProperty
                    {
                        EntityType = typeof (T).FullName,
                        Name = prop.Name,
                        Type = prop.PropertyType.FullName
                    });
                    //supposing to have ownerEntityId filled by magic
                    context.Save();

                    _knownProperties[prop.Name] = new PersistedPropertyInfo
                    {
                        Id = p.Id,
                        Info = typeof (T).GetProperty(p.Name)
                    };
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