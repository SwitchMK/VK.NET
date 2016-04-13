using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class GetPopularProperties
    {
        public bool EnglishOnly { get; set; } = true;
        public Genre Genre { get; set; } = Genre.Other;
        public int Offset { get; set; } = 0;
        public int Count { get; set; } = 100;

        public GetPopularProperties() { }
    }
}
