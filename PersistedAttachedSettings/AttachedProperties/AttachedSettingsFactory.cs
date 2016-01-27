using System;
using ImpromptuInterface;
using TypedConfig.Deserialization;
using TypedConfig.TypedAdapter;

namespace PersistedAttachedProperties.AttachedProperties
{
    public class AttachedSettingsFactory<TConfigType, TKey> where TConfigType : class
        where TKey : struct, IComparable<TKey>
    {
        public TConfigType Create(TKey entityId,
            TConfigType defaults,
            Func<IAttachedPropertyContext<TKey>> propertyInfos,
            ITypeDeserializer typeDeserializer)
        {
            var valueProvider = new DbPersistedPropertyValueProvider<TConfigType, TKey>(entityId, propertyInfos,
                //consider provide proper serializer implementation
                o => o.ToString(),
                defaults,
                typeDeserializer);
            return new FlatValuesToTypedClassAdapter(valueProvider).ActLike<TConfigType>();
        }
    }
}