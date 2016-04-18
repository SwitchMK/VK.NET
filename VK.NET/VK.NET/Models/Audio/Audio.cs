using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class Audio
    {
        public int aid { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public int lyrics_id { get; set; }
        public int genre_id { get; set; }
        public int date { get; set; }
        public int no_search { get; set; }
    }
}
