using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PersistedAttachedProperties.AttachedProperties;

namespace PersistedAttachedProperties.Persistance
{
    public class PropertyConfiguration : EntityTypeConfiguration<AttachedProperty>
    {
        public PropertyConfiguration()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.EntityType).HasMaxLength(Constants.Tables.Properties.EntityTypeLength).IsRequired().IsVariableLength();
            Property(t => t.Name).HasMaxLength(Constants.Tables.Properties.NameLength).IsRequired().IsVariableLength();
            Property(t => t.Type).HasMaxLength(Constants.Tables.Properties.DataTypeLength).IsRequired().IsVariableLength();
        }
    }

}