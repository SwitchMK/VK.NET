using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class DataProvider : IDataProvider
    {
        public async Task<string> GetJsonString(Method method, params Property[] properties)
        {
            HttpClient http = new HttpClient();
            string s = "https://api.vk.com/method/" + method.Name + "?need_user=0&" + 
                String.Join("&", properties.ToList()) + "&access_token=" + method.Token;
            var task = await http.GetAsync(s);
            var json = await task.Content.ReadAsStringAsync();

            return json;
        }
    }
}
