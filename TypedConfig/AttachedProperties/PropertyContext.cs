using System.Data.Entity;

namespace TypedConfig.AttachedProperties
{
    public class PropertyContext : DbContext
    {
        public DbSet<DomainEntity> DomainEntities { get; set; }
        public DbSet<AttachedPropertyValue> DomainEntityAttachedPropertyValues{ get; set; }
        public DbSet<AttachedProperty> DomainEntityAttachedProperties{ get; set; }
    }
}