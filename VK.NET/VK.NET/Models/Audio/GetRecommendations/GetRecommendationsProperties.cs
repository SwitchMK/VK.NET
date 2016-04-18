using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class GetRecommendationsProperties
    {
        public TargetAudio TargetAudio { get; set; }
        public int? UserId { get; set; } = null;
        public int Offset { get; set; } = 0;
        public int Count { get; set; } = 100;
        public bool Shuffle { get; set; } = true;

        public GetRecommendationsProperties() { }
    }

    public class TargetAudio
    {
        public int UserId { get; set; }
        public int AudioId { get; set; }

        public TargetAudio(int userId, int audioId)
        {
            UserId = userId;
            AudioId = audioId;
        }

        public override string ToString()
        {
            return string.Format($"{UserId}_{AudioId}");
        }
    }
}
