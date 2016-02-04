using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using PersistedAttachedProperties.AttachedProperties;

namespace PersistedAttachedProperties.Persistance
{
    public class AttachedPropertyValueLong : AttachedPropertyValue<long>
    {
    }
    public class AttachedPropertyValueInt : AttachedPropertyValue<int>
    {
    }
    /*public abstract class PropertyValuesConfigurationBase<T> : EntityTypeConfiguration<AttachedPropertyValue<T>>
        where T : struct
    {
        protected PropertyValuesConfigurationBase()
        {
            HasKey(v => new { v.PropertyId, v.EntityId });
            Property(t => t.PropertyId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Value).HasMaxLength(Constants.Tables.Values.ValueMaxLength).IsVariableLength().IsOptional();
        }
    }*/

    public class PropertyValuesConfigurationInt : EntityTypeConfiguration<AttachedPropertyValueInt>
    {
        public string TableName { get { return "AttachedPropertyValuesInt"; } }
        public PropertyValuesConfigurationInt()
        {
            HasKey(v => new {v.PropertyId, v.EntityId});
            Property(t => t.PropertyId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Value).HasMaxLength(Constants.Tables.Values.ValueMaxLength).IsVariableLength().IsOptional();
        }
    }

    public class PropertyValuesConfigurationLong :EntityTypeConfiguration<AttachedPropertyValueLong>
    
    {
        public string TableName { get { return "AttachedPropertyValuesBigInt"; } }
        public PropertyValuesConfigurationLong()
        {
            HasKey(v => new { v.PropertyId, v.EntityId });
            Property(t => t.PropertyId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.EntityId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(t => t.Value).HasMaxLength(Constants.Tables.Values.ValueMaxLength).IsVariableLength().IsOptional();
        }
    }
}