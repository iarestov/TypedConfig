using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using TypedConfig.Domain;
using TypedConfig.TypedAdapter;

namespace TypedConfig.Deserialization
{
    public class TypedPropertyDeserializer<T>:IPropertyValueProvider
    {
        private static readonly IDictionary<string, Type> PropertyTypes;

        private static readonly IDictionary<Type, Func<string, object>> TypeParsers;
        private readonly Func<string, string> _serializedPropertyProvider;

        public TypedPropertyDeserializer(Func<string,string> serializedPropertyProvider)
        {
            _serializedPropertyProvider = serializedPropertyProvider;
        }

        static TypedPropertyDeserializer()
        {
            PropertyTypes = typeof(T).GetProperties().ToDictionary(p => p.Name, p => p.PropertyType);

            TypeParsers = new Dictionary<Type, Func<string, object>>
            {
                {typeof(decimal), s => KnownTypeDeserializer.GetDecimal(s)},
                {typeof(string), s => s},
                {typeof(SubscribtionType), s => KnownTypeDeserializer.GetSubscribtionType(s)},
                {typeof(MailAddress), KnownTypeDeserializer.GetMailAddress},
            };
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

            if (!TypeParsers.TryGetValue(propertyType, out parser))
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
                return parser.Invoke(settingSerializedValue);
            }
            catch (Exception ex)
            {
                throw new ValueDeserializationException(propertyName, settingSerializedValue, propertyType, ex);
            }
        }
    }
}