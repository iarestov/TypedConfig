using System;
using System.Net.Mail;
using TypedConfig.Domain;

namespace TypedConfig.Deserialization
{
    public static class KnownTypeDeserializer
    {
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
    }
}