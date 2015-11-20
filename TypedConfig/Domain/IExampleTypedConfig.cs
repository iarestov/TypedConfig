using System.Net.Mail;

namespace TypedConfig.Domain
{
    public interface IExampleTypedConfig
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