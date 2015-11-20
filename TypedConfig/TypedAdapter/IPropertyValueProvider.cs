namespace TypedConfig.TypedAdapter
{
    public interface IPropertyValueProvider
    {
        object GetValue(string propertyName);
    }
}