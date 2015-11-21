using System;
using System.Net.Mail;
using TypedConfig.Deserialization;
using TypedConfig.Domain;

namespace TypedConfig
{
    public class GeneratedExampleConfig : IExampleTypedConfig
    {
        private readonly Func<string, string> _configProvider;

        public GeneratedExampleConfig(Func<string,string> configProvider)
        {
            _configProvider = configProvider;
        }

        public string FirstName 
        {
            get { return _configProvider.Invoke("FirstName"); }
        }

        public string LastName
        {
            get { return _configProvider.Invoke("FirstName"); }
        }

        public string MiddleName
        {
            get { return _configProvider.Invoke("MiddleName"); }
        }
        public decimal MounthlyFee
        {
            get { return KnownTypeDeserializer.GetDecimal(_configProvider.Invoke("MounthlyFee")); }
        }

        public decimal Balance
        {
            get { return KnownTypeDeserializer.GetDecimal(_configProvider.Invoke("Balance")); }
        }
        public MailAddress CustomerMail
        {
            get { return KnownTypeDeserializer.GetMailAddress(_configProvider.Invoke("CustomerMail")); }
        }
        public SubscriptionType Subscription
        {
            get { return KnownTypeDeserializer.GetSubscriptionType(_configProvider.Invoke("Subscription")); }
        }
    }
}