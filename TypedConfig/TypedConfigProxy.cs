using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Mail;
using ImpromptuInterface;

namespace TypedConfig
{
    public class TypedConfigProxy : DynamicObject
    {
        private static readonly IDictionary<Type, Func<string, object>> PropertyParsers = new Dictionary<Type, Func<string, object>>
        {
            {typeof(decimal), s => SettingStingsParser.GetDecimal(s)},
            {typeof(string), s => s},
            {typeof(SubscribtionType), s => SettingStingsParser.GetSubscribtionType(s)},
            {typeof(MailAddress), SettingStingsParser.GetMailAddress},
        };

        private readonly Func<string, string> _settingsProvider;

        private TypedConfigProxy(Func<string,string> settingsProvider )
        {
            _settingsProvider = settingsProvider;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = null;
            Func<string, object> parser;
            if (!PropertyParsers.TryGetValue(binder.ReturnType, out parser)) return false;

            var settingString = _settingsProvider.Invoke(binder.Name);
            result = parser.Invoke(settingString);
            return true;
        }

        public static T CreateProxyFor<T>(Func<string,string> settingsProvider) where T : class
        {
            return new TypedConfigProxy(settingsProvider).ActLike<T>();
        }
    }
}