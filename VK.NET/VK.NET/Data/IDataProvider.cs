using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VK.NET;

namespace VK.NET
{
    interface IDataProvider
    {
        Task<string> GetJsonString(Method method, params Property[] properties);
    }
}
