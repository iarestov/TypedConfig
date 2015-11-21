using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PersistedAttachedProperties.AttachedProperties;

namespace PersistedAttachedProperties.Persistance
{
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