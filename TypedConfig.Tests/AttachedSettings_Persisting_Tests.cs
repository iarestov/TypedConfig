using Domain;
using PerformanceSmokeTest;
using PersistedAttachedProperties.AttachedProperties;
using PersistedAttachedProperties.Persistance;
using Ploeh.AutoFixture;
using SpecsFor;

namespace TypedConfig.Tests
{
    public class AttachedSettings_Persisting_Tests : SpecsFor<AttachedSettingsFactory<IExampleTypedConfig>>
    {
        protected int EntityId;
        protected IExampleTypedConfig Config;
        protected DefaultExampleConfig DefaultExampleConfig;

        protected override void Given()
        {
            using (var context = new PropertyContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE AttachedProperties");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE AttachedPropertyValuesInt");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE AttachedPropertyValuesLong");
            }

            EntityId = (new Fixture()).Create<int>();
        }

        protected override void When()
        {
            DefaultExampleConfig = new DefaultExampleConfig();
            Config = SUT.Create(EntityId,
                DefaultExampleConfig,
                () => new ContextAdapter(new PropertyContext()),
                new KnownTypeDeserializer());
        }
    }
}