using System.Data.Entity;
using PersistedAttachedProperties.AttachedProperties;

namespace PersistedAttachedProperties.Persistance
{
    public class ContextAdapter : IAttachedPropertyContextLong
    {
        private readonly DbContext _context;

        public ContextAdapter(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IDbSet<AttachedProperty> Properties
        {
            get { return _context.Set<AttachedProperty>(); }
        }
        public IDbSet<AttachedPropertyValueLong> PropertyValues
        {
            get { return _context.Set<AttachedPropertyValueLong>(); }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}