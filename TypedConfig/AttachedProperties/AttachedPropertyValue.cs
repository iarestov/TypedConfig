using System.ComponentModel.DataAnnotations;

namespace TypedConfig.AttachedProperties
{
    public class AttachedPropertyValue
    {
        [Key]
        public int EntityId { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }
    }
}
