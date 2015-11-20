using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TypedConfig.AttachedProperties
{
    public class PropertyContext : DbContext
    {
        public DbSet<DomainEntity> DomainEntities { get; set; }
        public DbSet<AttachedPropertyValue> DomainEntityAttachedPropertyValues{ get; set; }
        public DbSet<AttachedProperty> DomainEntityAttachedProperties{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PropertyValuesConfiguration());
        } 
    }

    public class PropertyValuesConfiguration : EntityTypeConfiguration<AttachedPropertyValue>
    {
        public PropertyValuesConfiguration()
        {
            HasKey(v => new {v.PropertyId, v.EntityId});
            Property(t => t.PropertyId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}