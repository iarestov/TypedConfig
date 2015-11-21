using Ploeh.AutoFixture;
using SpecsFor;
using TypedConfig.AttachedProperties;
using TypedConfig.Domain;
using TypedConfig.Persistance;

namespace TypedConfig.Tests
{
    public class AttachedSettings_Persisting_Tests : SpecsFor<AttachedSettingsFactory<IExampleTypedConfig>>
    {
        protected int entityId;
        protected IExampleTypedConfig _config;
        protected DefaultExampleConfig _defaultExampleConfig;

        protected override void Given()
        {
            using (var context = new PropertyContext())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE AttachedProperties");
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE AttachedPropertyValues");
            }

            entityId = (new Fixture()).Create<int>();
        }

        protected override void When()
        {
            _defaultExampleConfig = new DefaultExampleConfig();
            _config = SUT.Create(entityId,
                _defaultExampleConfig,
                () => new ContextAdapter(new PropertyContext()));
        }

    }
}