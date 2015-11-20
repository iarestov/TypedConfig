using System.ComponentModel.DataAnnotations;

namespace TypedConfig.AttachedProperties
{
    public class AttachedProperty
    {
        [Key]
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}