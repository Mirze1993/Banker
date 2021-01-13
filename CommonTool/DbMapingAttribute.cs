using System;
using System.Collections.Generic;
using System.Text;

namespace CommonTool
{
    public class DbMapingAttribute: Attribute
    {
        public DbMapingAttribute(DbMap map)
        {
            Map = map;
        }
        public DbMap Map { get; }
    }

    public enum DbMap
    {
        Maping,noMaping
    }
}
