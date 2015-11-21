using System;
using System.Collections.Generic;
using System.Net.Mail;
using Domain;
using TypedConfig.Deserialization;

namespace PerformanceSmokeTest
{
    public class KnownTypeDeserializer: ITypeDeserializer
    {
        static KnownTypeDeserializer ()
        {
            TypeParsers = new Dictionary<Type, Func<string, object>>
            {
                {typeof(decimal), s => KnownTypeDeserializer.GetDecimal(s)},
                {typeof(string), s => s},
                {typeof(SubscriptionType), s => KnownTypeDeserializer.GetSubscriptionType(s)},
                {typeof(MailAddress), KnownTypeDeserializer.GetMailAddress},
            };
        }

        private static Dictionary<Type, Func<string, object>> TypeParsers { get; set; }

        public static decimal GetDecimal(string value)
        {
            return decimal.Parse(value);
        }

        public static MailAddress GetMailAddress(string value)
        {
            return new MailAddress(value);
        }

        public static SubscriptionType GetSubscriptionType(string value)
        {
            return (SubscriptionType)Enum.Parse(typeof(SubscriptionType), value);
        }

        public object Deserialize(Type type, string value)
        {
            return TypeParsers[type].Invoke(value);
        }

        public bool CanDeserialize(Type type)
        {
            return TypeParsers.ContainsKey(type);
        }
    }
}