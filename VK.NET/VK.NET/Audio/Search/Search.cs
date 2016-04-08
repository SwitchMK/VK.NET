using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public enum Sort
    {
        Data = 0,
        Duration = 1,
        Popular = 2
    }

    public class Search
    {
        public string TextRequest { get; set; }
        public bool AutoComplete { get; set; } 
        public bool WithLyrics { get; set; }
        public bool PerformerOnly { get; set; }
        public Sort SortMean { get; set; }
        public bool SearchOwn { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }

        public Search(string textRequest, 
            bool autoComplete = true, 
            bool withLyrics = false,
            bool performerOnly = false, 
            Sort sort = Sort.Popular, 
            bool searchOwn = true, 
            int offset = 0, 
            int count = 30)
        {
            TextRequest = textRequest;
            AutoComplete = autoComplete;
            WithLyrics = withLyrics;
            PerformerOnly = performerOnly;
            SortMean = sort;
            SearchOwn = searchOwn;
            Offset = offset;
            Count = count;
        }
    }
}
