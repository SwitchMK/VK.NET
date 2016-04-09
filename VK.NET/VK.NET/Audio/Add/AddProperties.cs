using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class AddProperties
    {
        public int? GroupId { get; set; }
        public int? AlbumId { get; set; }

        public AddProperties(int? groupId = null, int? albumId = null)
        {
            GroupId = groupId;
            AlbumId = albumId;
        }
    }
}
