using System.Data.Entity;
using TypedConfig.AttachedProperties;

namespace TypedConfig.Persistance
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
}