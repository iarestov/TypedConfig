using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TypedConfig.TypedAdapter;

namespace TypedConfig.Deserialization
{
    public class TypedPropertyDeserializer<T>:IPropertyValueProvider
    {
        private static readonly IDictionary<string, Type> PropertyTypes;

        private static readonly IDictionary<Type, Func<string, object>> TypeParsers;
        private readonly Func<string, string> _serializedPropertyProvider;
        private readonly ITypeDeserializer _typeDeserializer;

        public TypedPropertyDeserializer(Func<string,string> serializedPropertyProvider,
                                         ITypeDeserializer typeDeserializer)
        {
            _typeDeserializer = typeDeserializer;
            _serializedPropertyProvider = serializedPropertyProvider;
        }

        static TypedPropertyDeserializer()
        {
            PropertyTypes = typeof(T).GetProperties().ToDictionary(p => p.Name, p => p.PropertyType);
        }

        public object GetValue(string propertyName)
        {
            string settingSerializedValue;

            Type propertyType;
            Func<string, object> parser;
            if (!PropertyTypes.TryGetValue(propertyName, out propertyType))
            {
                throw new UnregisteredPropertyExcepiton(propertyName);
            }

            if (!_typeDeserializer.CanDeserialize(propertyType))
            {
                throw new UnregisteredTypeDesirializerExcepiton(propertyName, propertyType);
            }

            try
            {
                settingSerializedValue = _serializedPropertyProvider.Invoke(propertyName);
            }
            catch (Exception ex)
            {
                throw new PropertyValueCannotBeObtainedException(propertyName, ex);
            }

            try
            {
                return _typeDeserializer.Deserialize(propertyType,settingSerializedValue);
            }
            catch (Exception ex)
            {
                throw new ValueDeserializationException(propertyName, settingSerializedValue, propertyType, ex);
            }
        }
    }
}