using System;
using System.Data.Entity;
using PersistedAttachedProperties.Persistance;

namespace PersistedAttachedProperties.AttachedProperties
{
    public interface IAttachedPropertyContext<T> : IDisposable
    {
        IDbSet<AttachedProperty> Properties { get; }
        IDbSet<AttachedPropertyValue<T>> PropertyValues { get; }
        void Save();
    }

    public interface IAttachedPropertyContextLong : IDisposable
    {
        IDbSet<AttachedProperty> Properties { get; }
        IDbSet<AttachedPropertyValueLong> PropertyValues { get; }
        void Save();
    }


    public interface IAttachedPropertyContextInt : IDisposable
    {
        IDbSet<AttachedProperty> Properties { get; }
        IDbSet<AttachedPropertyValueInt> PropertyValues { get; }
        void Save();
    }
}