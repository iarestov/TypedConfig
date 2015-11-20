using System;
using System.Data.Entity;

namespace TypedConfig.AttachedProperties
{
    public interface IAttachedPropertyContext:IDisposable
    {
        IDbSet<AttachedProperty> Properties { get; }
        IDbSet<AttachedPropertyValue> PropertyValues { get; }
        void Save();
    }
}