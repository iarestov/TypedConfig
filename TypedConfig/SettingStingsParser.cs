using System;
using System.Net.Mail;

namespace TypedConfig
{
    public static class SettingStingsParser
    {
        public static decimal GetDecimal(string value)
        {
            return decimal.Parse(value);
        }

        public static MailAddress GetMailAddress(string value)
        {
            return new MailAddress(value);

        }

        public static SubscribtionType GetSubscribtionType(string value)
        {
            return (SubscribtionType)Enum.Parse(typeof(SubscribtionType), value);
        }
    }
}