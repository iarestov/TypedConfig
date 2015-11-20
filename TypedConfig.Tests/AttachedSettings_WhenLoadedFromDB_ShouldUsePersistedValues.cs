using System.Net.Mail;
using System.Reflection;
using NUnit.Framework;
using Ploeh.AutoFixture;
using SpecsFor;
using TypedConfig.AttachedProperties;
using TypedConfig.Domain;

namespace TypedConfig.Tests
{
    [TestFixture]
    public class AttachedSettings_WhenLoadedFromDB_ShouldUsePersistedValues:AttachedSettings_Persisting_Tests 
    {
        private ConfigGeneratorDummy _existingConfig;

        class ConfigGeneratorDummy : IExampleTypedConfig
        {
            public string FirstName { get;  set; }
            public string LastName { get;  set; }
            public string MiddleName { get;  set; }
            public decimal MounthlyFee { get;  set; }
            public decimal Balance { get;  set; }
            public MailAddress CustomerMail { get;  set; }
            public SubscribtionType Subscription { get;  set; }
        }

        protected override void Given()
        {
            base.Given();

            var fixture = (new Fixture());
            _existingConfig = fixture.Create<ConfigGeneratorDummy>();

            using (var context = new PropertyContext())
            {
                foreach (var prop in typeof(IExampleTypedConfig).GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    var p =  context.DomainEntityAttachedProperties.Add(new AttachedProperty()
                    {
                        EntityType = typeof (DomainEntity).FullName,
                        Name = prop.Name,
                        Type = prop.PropertyType.FullName
                    });

                    context.SaveChanges();

                    context.DomainEntityAttachedPropertyValues.Add(new AttachedPropertyValue()
                    {
                        EntityId = entityId,
                        PropertyId = p.Id,
                        Value = prop.GetValue(_existingConfig).ToString()
                    });
                }

                context.SaveChanges();
            }
        }

        [Test]
        public void Then_FirstName_is_taken_from_persisted_storage()
        {
            Assert.AreEqual(_existingConfig.FirstName, _config.FirstName);
        }

        [Test]
        public void Then_Mail_is_taken_from_persisted_storage()
        {
            Assert.AreEqual(_existingConfig.CustomerMail, _config.CustomerMail);
        }
    }
}