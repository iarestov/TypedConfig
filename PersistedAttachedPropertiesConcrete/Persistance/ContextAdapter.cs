using System.Data.Entity;
using PersistedAttachedProperties.AttachedProperties;

namespace PersistedAttachedProperties.Persistance
{
    public class ContextAdapter : IAttachedPropertyContext<int>
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
        public IDbSet<AttachedPropertyValue<int>> PropertyValues
        {
            get { return GetPropertyValues<int>(); }
        }
        private IDbSet<AttachedPropertyValue<T>> GetPropertyValues<T>()
        {
            return _context.Set<AttachedPropertyValue<T>>();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}