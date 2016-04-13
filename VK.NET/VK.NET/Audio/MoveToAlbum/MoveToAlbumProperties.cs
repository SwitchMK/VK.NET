using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class MoveToAlbumProperties
    {
        public int? AlbumId { get; set; }
        public int? GroupId { get; set; }
        public ICollection<int> AudioIds { get; set; }

        public MoveToAlbumProperties(int? albumId = null, 
            int? groupId = null,
            params int[] audioIds)
        {
            AlbumId = albumId;
            GroupId = groupId;
            AudioIds = audioIds;
        }
    }
}
