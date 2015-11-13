using System.Net.Mail;

namespace TypedConfig
{
    public interface ITypedConcreteConfig
    {
        string FirstName { get; }
        string LastName { get; }
        string MiddleName { get; }
        decimal MounthlyFee { get; }
        decimal Balance { get; }  
        MailAddress CustomerMail { get; }
        SubscribtionType Subscription { get; }
    }
}