using System;
using System.Data.Entity;

namespace PersistedAttachedProperties.AttachedProperties
{
    public interface IAttachedPropertyContext<T> : IDisposable
    {
        IDbSet<AttachedProperty> Properties { get; }
        IDbSet<AttachedPropertyValue<T>> PropertyValues { get; }
        void Save();
    }
}