using System;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using PersistedAttachedProperties.Persistance;
using TypedConfig.Deserialization;

namespace PersistedAttachedProperties.AttachedProperties
{
    public class AttachedDataRepositoryLong : IAttachedDataRepositoryLong
    {
        private readonly IAttachedPropertyContextLong _ctx;
        private readonly IAttachedDataMapper _mapper;

        public AttachedDataRepositoryLong(IAttachedPropertyContextLong ctx, IAttachedDataMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }

        public T GetValue<T>(long id, T defauts = null) where T : class, IAttachedData
        {
            var props = _mapper.GetProperties<T>();
            var deserializer = _mapper.GetDeserializers<T>();

            var propIds = props.Values.Select(x => x.Id).ToArray();
            var persistedProps = _ctx.PropertyValues.Where(x => x.EntityId == id && propIds.Contains(x.PropertyId))
                .ToDictionary(x => x.PropertyId, x => x.Value);

            if (persistedProps.Count == 0)
                return defauts;

            var result = Activator.CreateInstance<T>();

            foreach (var prop in props)
            {
                if (prop.Key.Equals(Constants.Mapping.AttachedDataIdentityProperyName)) continue;

                var propType = prop.Value.Info.PropertyType;

                if (!deserializer.CanDeserialize(propType))
                    throw new UnregisteredTypeDesirializerExcepiton(prop.Key, propType);

                var reflectedPropertyInfo = prop.Value.Info;

                if (defauts != null)
                {
                    reflectedPropertyInfo.SetValue(result, reflectedPropertyInfo.GetValue(defauts));
                }

                string serializedVal;
                if (persistedProps.TryGetValue(prop.Value.Id, out serializedVal))
                {
                    reflectedPropertyInfo.SetValue(result,
                        deserializer.Deserialize(propType, serializedVal));
                }
            }

            result.Id = id;

            return result;
        }

        public void SetValue<T>(T value) where T : class, IAttachedData
        {
            var props = _mapper.GetProperties<T>();
            var serializer = _mapper.GetSerializers<T>();

            foreach (var old in _ctx.PropertyValues.Where(x => x.EntityId == value.Id))
            {
                _ctx.PropertyValues.Remove(old);
            }

            foreach (var prop in props)
            {
                var reflectedPropertyInfo = prop.Value.Info;
                _ctx.PropertyValues.Add(new AttachedPropertyValueLong()
                {
                    Value = serializer.Invoke(reflectedPropertyInfo.GetValue(value)),
                    PropertyId = prop.Value.Id,
                    EntityId = value.Id
                });
            }

            _ctx.Save();

        }
    }
}
