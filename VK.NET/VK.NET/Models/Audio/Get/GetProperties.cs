using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class GetProperties
    {
        public int? OwnerId { get; set; }
        public int? AlbumId { get; set; }
        public int[] AudioIds { get; set; }
        public bool NeedUser { get; set; }
        public int Offset { get; set; }
        public int? Count { get; set; }

        public GetProperties(int? ownerId = null, 
            int? albumId = null,
            int[] audioIds = null, 
            bool needUser = false, 
            int offset = 0,
            int? count = null)
        {
            OwnerId = ownerId;
            AlbumId = albumId;
            AudioIds = audioIds;
            NeedUser = needUser;
            Offset = offset;
            Count = count;
        }
    }
}
