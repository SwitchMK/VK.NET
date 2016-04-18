using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class AddProperties
    {
        public int AudioId { get; set; }
        public int OwnerId { get; set; }
        public int? GroupId { get; set; }
        public int? AlbumId { get; set; }

        public AddProperties(
            int audioId,
            int ownerId,
            int? groupId = null, 
            int? albumId = null)
        {
            AudioId = audioId;
            OwnerId = ownerId;
            GroupId = groupId;
            AlbumId = albumId;
        }
    }
}
