using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypedConfig.ExternalSettings
{
    class AttachedPropertyValue
    {
        public int EntityId { get; set; }
        public int Id { get; set; }
        public string Value { get; set; }
    }

    class AttachedProperty
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    class PropertyContext : DbContext
    {
        
    }
}
