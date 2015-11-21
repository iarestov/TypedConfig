using System.Net.Mail;

namespace Domain
{
    public class DefaultExampleConfig : IExampleTypedConfig
    {
        public string FirstName { get { return "Undefined"; }}
        public string LastName { get { return "Undefined"; } }
        public string MiddleName { get { return "Undefined"; } }
        public decimal MounthlyFee { get { return 0; } }
        public decimal Balance { get { return 0; } }
        public MailAddress CustomerMail { get {return new MailAddress("fake@test.net");}}
        public SubscriptionType Subscription { get{return SubscriptionType.Free;} }
    }
}