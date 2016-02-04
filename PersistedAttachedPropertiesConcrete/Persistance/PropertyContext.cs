using System.Data.Entity;
using PersistedAttachedProperties.AttachedProperties;

namespace PersistedAttachedProperties.Persistance
{
    public class PropertyContext : DbContext
    {
        public DbSet<DomainEntity> DomainEntities { get; set; }
        public DbSet<AttachedPropertyValueLong> DomainEntityAttachedPropertyValuesLong { get; set; }
        public DbSet<AttachedPropertyValueInt> DomainEntityAttachedPropertyValuesInt { get; set; }
        public DbSet<AttachedProperty> DomainEntityAttachedProperties { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PropertyConfiguration());
            modelBuilder.Configurations.Add(new PropertyValuesConfigurationInt());
            modelBuilder.Configurations.Add(new PropertyValuesConfigurationLong());
        }
    }
}