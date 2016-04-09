using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class DataProvider
    {
        // Returned json string by calling some method
        public async Task<string> GetJsonString(Method method, params Property[] properties)
        {
            var http = new HttpClient();

            var requestString = "https://api.vk.com/method/" + method.Name + "?" + 
                String.Join("&", properties.ToList()) + "&access_token=" + method.Token;

            var response = await http.GetAsync(requestString);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }
    }
}
