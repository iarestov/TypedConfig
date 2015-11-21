using NUnit.Framework;
using SpecsFor;
using TypedConfig.AttachedProperties;
using TypedConfig.Domain;

namespace TypedConfig.Tests
{
    [TestFixture]
    internal class AttachedSettings_WhenSaving_ShouldPersistValuesInDB : SpecsFor<AttachedSettingsFactory<IExampleTypedConfig>>
    {

    }
}