namespace TypedConfig
{
    public interface IPropertyValueProvider
    {
        object GetValue(string propertyName);
    }
}