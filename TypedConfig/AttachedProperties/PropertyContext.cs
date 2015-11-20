using System.Data.Entity;

namespace TypedConfig.AttachedProperties
{
    public class PropertyContext : DbContext, IAttachedPropertyContext
    {
        public DbSet<DomainEntity> DomainEntities { get; set; }
        public DbSet<AttachedPropertyValue> DomainEntityAttachedPropertyValues{ get; set; }
        public DbSet<AttachedProperty> DomainEntityAttachedProperties{ get; set; }

        public IDbSet<AttachedProperty> Properties
        {
            get { return DomainEntityAttachedProperties; }
        }

        public IDbSet<AttachedPropertyValue> PropertyValues
        {
            get { return DomainEntityAttachedPropertyValues; }
        }

        public void Save()
        {
            this.SaveChanges();
        }
    }
}