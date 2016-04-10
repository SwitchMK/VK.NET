using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class ReorderProperties
    {
        public int? OwnerId { get; set; }
        public int? Before { get; set; }
        public int? After { get; set; }

        public ReorderProperties(int? ownerId = null,
            int? before = null,
            int? after = null)
        {
            OwnerId = ownerId;
            Before = before;
            After = after;
        }
    }
}
