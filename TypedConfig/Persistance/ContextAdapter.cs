using System.Data.Entity;
using TypedConfig.AttachedProperties;

namespace TypedConfig.Persistance
{
    public class ContextAdapter : IAttachedPropertyContext
    {
        private readonly DbContext _context;

        public void Dispose()
        {
            _context.Dispose();
        }

        public IDbSet<AttachedProperty> Properties { get { return _context.Set<AttachedProperty>(); }}
        public IDbSet<AttachedPropertyValue> PropertyValues { get { return _context.Set<AttachedPropertyValue>(); }}
        public void Save()
        {
            _context.SaveChanges();
        }

        public ContextAdapter(DbContext context)
        {
            _context = context;
        }
    }
}