using System.Net.Mail;

namespace Domain
{
    public interface IExampleTypedConfig
    {
        string FirstName { get; }
        string LastName { get; }
        string MiddleName { get; }
        decimal MounthlyFee { get; }
        decimal Balance { get; }  
        MailAddress CustomerMail { get; }
        SubscriptionType Subscription { get; }
    }
}