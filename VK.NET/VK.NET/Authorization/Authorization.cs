using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VK.NET
{
    public class Authorization
    {
        public int Client_Id { get; set; }
        public string Redirect_Uri { get; set; }
        public string Display { get; set; }
        public ICollection<string> Scope { get; set; }
        public string Response_Type { get; set; }
        public double Version { get; set; }
        public string State { get; set; }

        // Constructor for creating an instance of
        // Authorization where only client_id isn't optional
        public Authorization(int client_id, string redirect_uri = "https://oauth.vk.com/blank.html",
             string display = "page", string response_type = "token",
             double version = 5.50, string state = "", params string[] scope)
        {
            Client_Id = client_id;
            Redirect_Uri = redirect_uri;
            Display = display;
            Scope = scope;
            Response_Type = response_type;
            Version = version;
            State = state;
        }

        // Creating request string to push to browser.
        public string Request()
        {
            return "https://oauth.vk.com/authorize?client_id=" + Client_Id +
                "&scope=" + String.Join(",", Scope) +
                "&redirect_uri=" + Redirect_Uri + "&display=" + Display +
                "&v=" + Version + "&response_type=" + Response_Type;
        }

        // Taking token string and id string for
        // identification from returned URL.
        public static void Authorize(string url, out string token, out string id, out bool auth)
        {
            try
            {
                string l = url.Split('#')[1];
                if (l[0] == 'a')
                {
                    token = l.Split('&')[0].Split('=')[1];
                    id = l.Split('=')[3];
                    auth = true;
                }
                else
                    throw new Exception();
            }
            catch
            {
                token = id = "";
                auth = false;
            }
        }
    }
}
