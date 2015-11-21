using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using ExpectedObjects;
using ImpromptuInterface;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Should;
using SpecsFor;
using TypedConfig.TypedAdapter;

namespace TypedConfig.Tests
{
    [TestFixture]
    public class TypedAdapter_WhenFakesType_ShouldProvideValues:SpecsFor<ValueCollectionToTypedClassAdapter>
    {
        private ITestTypedInterface _fakedClass;
        private TestTypedClass _testValues;

        public interface ITestTypedInterface
        {
            string StringValue { get; }
            decimal DecValue { get; }
            MailAddress MailValue { get; }
            object ObjValue { get; }
        }

        class TestTypedClass:ITestTypedInterface
        {
            public string StringValue { get; set; }
            public decimal DecValue { get; set; }
            public MailAddress MailValue { get; set; }
            public object ObjValue { get; set; }
        }

        protected override void Given()
        {
            _testValues = (new Fixture()).Create<TestTypedClass>();
            GetMockFor<IPropertyValueProvider>().Setup(p => p.GetValue("StringValue")).Returns(_testValues.StringValue);
            GetMockFor<IPropertyValueProvider>().Setup(p => p.GetValue("DecValue")).Returns(_testValues.DecValue);
            GetMockFor<IPropertyValueProvider>().Setup(p => p.GetValue("MailValue")).Returns(_testValues.MailValue);
            GetMockFor<IPropertyValueProvider>().Setup(p => p.GetValue("ObjValue")).Returns(_testValues.ObjValue);
        }

        protected override void When()
        {
            _fakedClass = SUT.ActLike<ITestTypedInterface>();
        }

        [Test]
        public void StringValue_Should_be_provided()
        {
            Assert.AreEqual(_testValues.StringValue, _fakedClass.StringValue);
        }

        [Test]
        public void ObjValue_Should_be_provided()
        {
            var objValue = _fakedClass.ObjValue;
            Assert.AreEqual(_testValues.ObjValue, objValue);
        }

        [Test]
        public void MailValue_Should_be_provided()
        {
            Assert.AreEqual(_testValues.MailValue, _fakedClass.MailValue);
        }

        [Test]
        public void DecimalValue_Should_be_provided()
        {
            Assert.AreEqual(_testValues.DecValue, _fakedClass.DecValue);
        }
    }


}
