using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class Method
    {
        public Method(string name)
        {
            Name = name;
        }

        public Method(string name, string token = "") : this(name)
        {
            Token = token;
        }

        public string Name { get; set; }
        public string Token { get; set; } = "";
    }
}
