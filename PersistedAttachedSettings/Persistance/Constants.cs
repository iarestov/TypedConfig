using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistedAttachedProperties.Persistance
{
    internal class Constants
    {
        public static class Tables
        {
            public static class Properties
            {
                public const int DataTypeLength = 256;
                public const int EntityTypeLength = 256;
                public const int NameLength = 128;
            }
            public static class Values
            {
                public const int ValueMaxLength = 1024;
            }
        }

        public static class Mapping
        {
            public const string AttachedDataIdentityProperyName = "Id";
        }
    }
}
