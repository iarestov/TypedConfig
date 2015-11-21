using System.Dynamic;

namespace TypedConfig.TypedAdapter
{
    public class ValueCollectionToTypedClassAdapter : DynamicObject
    {
        private readonly IPropertyValueProvider _settingsProvider;

        public ValueCollectionToTypedClassAdapter(IPropertyValueProvider settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = _settingsProvider.GetValue(binder.Name);
            return true;
        }
    }
}