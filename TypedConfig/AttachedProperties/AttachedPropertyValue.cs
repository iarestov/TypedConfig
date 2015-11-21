﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TypedConfig.AttachedProperties
{
    public class AttachedPropertyValue
    {
        public int EntityId { get; set; }
        public int PropertyId { get; set; }
        public string Value { get; set; }
    }
}
