using System;
using System.Net.Mail;

namespace TypedConfig
{
    public class GeneratedDomainRelatedConfig : IDomainRelatedConfig
    {
        private readonly Func<string, string> _configProvider;

        public GeneratedDomainRelatedConfig(Func<string,string> configProvider)
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
        public SubscribtionType Subscription
        {
            get { return KnownTypeDeserializer.GetSubscribtionType(_configProvider.Invoke("Subscription")); }
        }
    }
}