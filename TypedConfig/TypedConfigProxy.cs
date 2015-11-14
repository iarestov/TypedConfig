using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Mail;
using ImpromptuInterface;

namespace TypedConfig
{

    public class TypedConfigProxy<T> : DynamicObject where T:class
    {
        private static readonly IDictionary<Type, Func<string, object>> TypeParsers;
        private static readonly IDictionary<string, Type> PropertyTypes;
        private readonly Func<string, string> _settingsProvider;

        static TypedConfigProxy()
        {
            PropertyTypes = typeof(T).GetProperties().ToDictionary(p => p.Name, p => p.PropertyType);
            TypeParsers = new Dictionary<Type, Func<string, object>>
            {
                {typeof(decimal), s => SettingStingsParser.GetDecimal(s)},
                {typeof(string), s => s},
                {typeof(SubscribtionType), s => SettingStingsParser.GetSubscribtionType(s)},
                {typeof(MailAddress), SettingStingsParser.GetMailAddress},
            };
        }
        private TypedConfigProxy(Func<string,string> settingsProvider)
        {
            _settingsProvider = settingsProvider;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            Func<string, object> parser;
            Type propertyType;
            result = null;
            var settingName = binder.Name;
            if(!PropertyTypes.TryGetValue(settingName,out propertyType)) return false;
            if(!TypeParsers.TryGetValue(propertyType, out parser)) return false;

            string settingSerializedValue;
            try
            {
                settingSerializedValue = _settingsProvider.Invoke(settingName);
            }
            catch (Exception ex)
            {
                throw new SettingValueCannotBeFoundException(settingName, ex);
            }
            
            try
            {
                result = parser.Invoke(settingSerializedValue);
            }
            catch (Exception ex)
            {
                throw new SettingValueParseException(settingName, settingSerializedValue, propertyType, ex);
            }
            
            return true;
        }

        public static T Create(Func<string, string> settingsProvider)
        {
            return new TypedConfigProxy<T>(settingsProvider).ActLike<T>();
        }
    }
}