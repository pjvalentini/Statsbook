using System;
using System.Collections.Generic;
using System.Text;

namespace StatsBook
{
    public class NamedObject
    {
        public NamedObject(string name)
        {
            // this ctor has to take a name parameter
            Name = name;
        }

        // This is an auto property, the field name will automatically be generated here.
        public string Name
        {
            get;
            set;
        }
    }
}
