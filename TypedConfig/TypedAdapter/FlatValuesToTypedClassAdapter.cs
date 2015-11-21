using System.Dynamic;

namespace TypedConfig.TypedAdapter
{
    public class FlatValuesToTypedClassAdapter : DynamicObject
    {
        private readonly IPropertyValueProvider _settingsProvider;

        public FlatValuesToTypedClassAdapter(IPropertyValueProvider settingsProvider)
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