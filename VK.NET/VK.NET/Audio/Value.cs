﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public struct Value
    {
        public int AudioId { get; set; }
        public int OwnerId { get; set; }

        public Value(int ownerId, int audioId)
        {
            AudioId = audioId;
            OwnerId = ownerId;
        }

        public override string ToString()
        {
            return string.Format($"{OwnerId}_{AudioId}");
        }
    }
}
