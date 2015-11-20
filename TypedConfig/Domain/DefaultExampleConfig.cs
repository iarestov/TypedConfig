using System.Net.Mail;

namespace TypedConfig.Domain
{
    public class DefaultExampleConfig : IExampleTypedConfig
    {
        public string FirstName { get { return "Undefined"; }}
        public string LastName { get { return "Undefined"; } }
        public string MiddleName { get { return "Undefined"; } }
        public decimal MounthlyFee { get { return 0; } }
        public decimal Balance { get { return 0; } }
        public MailAddress CustomerMail { get {return new MailAddress("fake@test.net");}}
        public SubscribtionType Subscription { get{return SubscribtionType.Free;} }
    }
}