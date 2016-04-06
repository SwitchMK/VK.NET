using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class Property
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public Property(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}={1}", Name, Value);
        }
    }
}
