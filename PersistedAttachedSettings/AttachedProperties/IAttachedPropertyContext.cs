using System;
using System.Data.Entity;

namespace PersistedAttachedProperties.AttachedProperties
{
    public interface IAttachedPropertyContext : IDisposable//, IAttachedPropertyContextTypedTables<int>, IAttachedPropertyContextTypedTables<long>
    {
        IDbSet<AttachedProperty> Properties { get; }
        IDbSet<AttachedPropertyValue<T>> GetPropertyValues<T>();
        void Save();
    }
}