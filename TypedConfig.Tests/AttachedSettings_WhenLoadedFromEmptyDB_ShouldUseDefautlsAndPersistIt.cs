using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Domain;
using NUnit.Framework;
using PersistedAttachedProperties.Persistance;

namespace TypedConfig.Tests
{
    [TestFixture]
    class AttachedSettings_WhenLoadedFromEmptyDB_ShouldUseDefautlsAndPersistIt:AttachedSettings_Persisting_Tests 
    {
    
        
        [Test]
        public void Then_FirstName_should_be_default()
        {
            Assert.AreEqual(DefaultExampleConfig.FirstName, Config.FirstName);
        }

        [Test]
        public void Then_Mail_should_be_default()
        {
            Assert.AreEqual(DefaultExampleConfig.CustomerMail, Config.CustomerMail);
        }

        [Test]
        public void Then_Db_should_know_firstName_property()
        {

            using (var context = new PropertyContext())
            {
                var firstName = Config.FirstName;
                Assert.NotNull(context.DomainEntityAttachedProperties.Single(p =>
                    p.Type == typeof (string).FullName &&
                    p.EntityType == typeof (IExampleTypedConfig).FullName &&
                    p.Name == "FirstName"
                    ));
            }
        }

        [Test]
        public void Then_Db_should_know_mail_property()
        {
            using (var context = new PropertyContext())
            {
                var mail = Config.CustomerMail;
                Assert.NotNull(context.DomainEntityAttachedProperties.Single(p =>
                    p.Type == typeof (MailAddress).FullName &&
                    p.EntityType == typeof (IExampleTypedConfig).FullName &&
                    p.Name == "CustomerMail"
                    ));
            }
        }

        [Test]
        public void Then_Db_should_contain_default_value_for_first_name_property()
        {
            using (var context = new PropertyContext())
            {
                var firstName = Config.FirstName;
                Assert.NotNull(context.DomainEntityAttachedProperties.Single(p =>
                    p.Type == typeof (string).FullName &&
                    p.EntityType == typeof (IExampleTypedConfig).FullName &&
                    p.Name == "FirstName"
                    ));
            }
        }

        [Test]
        public void Then_Db_should_contain_default_value_for_mail_property()
        {
            using (var context = new PropertyContext())
            {
                var mail = Config.CustomerMail;
                Assert.NotNull(context.DomainEntityAttachedProperties.Single(p =>
                    p.Type == typeof (MailAddress).FullName &&
                    p.EntityType == typeof (IExampleTypedConfig).FullName &&
                    p.Name == "CustomerMail"
                    ));
            }
        }
    }
}
