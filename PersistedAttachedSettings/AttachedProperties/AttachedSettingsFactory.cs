using System;
using ImpromptuInterface;
using TypedConfig.Deserialization;
using TypedConfig.TypedAdapter;

namespace PersistedAttachedProperties.AttachedProperties
{
    public class AttachedSettingsFactory<TConfigType> where TConfigType : class
    {
        public TConfigType Create(long entityId,
            TConfigType defaults,
            Func<IAttachedPropertyContextLong> propertyInfos,
            ITypeDeserializer typeDeserializer)
        {
            var valueProvider = new DbPersistedPropertyValueProvider<TConfigType>(entityId, propertyInfos,
                //consider provide proper serializer implementation
                o => o.ToString(),
                defaults,
                typeDeserializer);
            return new FlatValuesToTypedClassAdapter(valueProvider).ActLike<TConfigType>();
        }
    }
}