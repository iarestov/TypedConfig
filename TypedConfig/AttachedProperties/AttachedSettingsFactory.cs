using System;
using ImpromptuInterface;
using TypedConfig.TypedAdapter;

namespace TypedConfig.AttachedProperties
{
    public class AttachedSettingsFactory<TConfigType> where TConfigType : class
    {
        public TConfigType Create(int entityId,
                                  TConfigType defaults, 
                                  Func<IAttachedPropertyContext> propertyInfos)
        {
            var valueProvider = new DbPersistedPropertyValueProvider<TConfigType>(entityId,propertyInfos, o => o.ToString(), defaults);
            return new FlatValuesToTypedClassAdapter(valueProvider).ActLike<TConfigType>();
        }
    }
}