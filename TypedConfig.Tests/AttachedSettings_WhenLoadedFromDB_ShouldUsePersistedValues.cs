using System.Net.Mail;
using NUnit.Framework;
using Ploeh.AutoFixture;
using SpecsFor;
using TypedConfig.AttachedProperties;
using TypedConfig.Domain;

namespace TypedConfig.Tests
{
    [TestFixture]
    internal class AttachedSettings_WhenLoadedFromDB_ShouldUsePersistedValues: SpecsFor<AttachedSettingsFactory<IExampleTypedConfig>>
    {
        class ConfigGeneratorDummy : IExampleTypedConfig
        {
            public string FirstName { get; private set; }
            public string LastName { get; private set; }
            public string MiddleName { get; private set; }
            public decimal MounthlyFee { get; private set; }
            public decimal Balance { get; private set; }
            public MailAddress CustomerMail { get; private set; }
            public SubscribtionType Subscription { get; private set; }
        }

        //private int entityId;
        //private DefaultExampleConfig _config;
        //private DefaultExampleConfig _defaultExampleConfig;

        //protected override void When()
        //{
        //    entityId = (new Fixture()).Create<int>();
        //    _defaultExampleConfig = new DefaultExampleConfig();
        //    _config = SUT.Create(entityId,
        //        _defaultExampleConfig,
        //        () => new PropertyContext().DomainEntityAttachedProperties,
        //        () => new PropertyContext().DomainEntityAttachedPropertyValues);
        //}

        //protected override void Given()
        //{
        //    using (var context = new PropertyContext())
        //    {
                
        //    }
        //}
    }
}