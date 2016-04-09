using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class EditProperties
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Genre Genre { get; set; }
        public bool NoSearch { get; set; }

        public EditProperties(string artist = "", string title = "",
            string text = "", Genre genre = Genre.Other, bool noSearch = false)
        {
            Artist = artist;
            Title = title;
            Text = text;
            Genre = genre;
            NoSearch = noSearch;
        }
    }
}
