using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ploeh.AutoFixture;
using SpecsFor;
using TypedConfig.AttachedProperties;
using TypedConfig.Domain;

namespace TypedConfig.Tests
{
    [TestFixture]
    class AttachedSettings_WhenLoadedFromEmptyDB_ShouldUseDefautlsAndPersistIt : SpecsFor<AttachedSettingsFactory<IExampleTypedConfig>>
    {
        private int entityId;
        private IExampleTypedConfig _config;
        private DefaultExampleConfig _defaultExampleConfig;

        [TestFixtureSetUp]
        public void ClearDB()
        {
            using (var context = new PropertyContext())
            {
                context.DomainEntityAttachedPropertyValues.RemoveRange(context.DomainEntityAttachedPropertyValues);
                context.DomainEntityAttachedProperties.RemoveRange(context.DomainEntityAttachedProperties);
            }
        }

        protected override void When()
        {

           
            entityId = (new Fixture()).Create<int>();
            _defaultExampleConfig = new DefaultExampleConfig();
            _config = SUT.Create(entityId,
                                 _defaultExampleConfig,
                                 () => new ContextAdapter(new PropertyContext()));
        }

        
        [Test]
        public void Then_FirstName_should_be_default()
        {
            Assert.AreEqual(_defaultExampleConfig.FirstName, _config.FirstName);
        }

        [Test]
        public void Then_Mail_should_be_default()
        {
            Assert.AreEqual(_defaultExampleConfig.CustomerMail, _config.CustomerMail);
        }

        [Test]
        public void Then_Db_should_know_firstName_property()
        {
            var context = new PropertyContext();
            Assert.NotNull(context.DomainEntityAttachedProperties.Single(p => 
                p.Type == typeof(string).FullName && 
                p.EntityType == typeof(IExampleTypedConfig).FullName && 
                p.Name == "FirstName"
                ));
        }

        [Test]
        public void Then_Db_should_know_mail_property()
        {
            var context = new PropertyContext();
            Assert.NotNull(context.DomainEntityAttachedProperties.Single(p =>
                p.Type == typeof(MailAddress).FullName &&
                p.EntityType == typeof(IExampleTypedConfig).FullName &&
                p.Name == "CustomerMail"
                ));
        }

        [Test]
        public void Then_Db_should_contain_default_value_for_first_name_property()
        {
            var context = new PropertyContext();
            Assert.NotNull(context.DomainEntityAttachedProperties.Single(p =>
                p.Type == typeof(string).FullName &&
                p.EntityType == typeof(IExampleTypedConfig).FullName &&
                p.Name == "FirstName"
                ));
        }

        [Test]
        public void Then_Db_should_contain_default_value_for_mail_property()
        {
            var context = new PropertyContext();
            Assert.NotNull(context.DomainEntityAttachedProperties.Single(p =>
                p.Type == typeof(MailAddress).FullName &&
                p.EntityType == typeof(IExampleTypedConfig).FullName &&
                p.Name == "CustomerMail"
                ));
        }
    }
}
