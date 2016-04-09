using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace VK.NET
{
    // Audio entity
    public partial class Audio
    {
        // Properties of audio entity
        public int aid { get; set; }
        public int owner_id { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public int duration { get; set; }
        public string url { get; set; }
        public int lyrics_id { get; set; }
        public int genre_id { get; set; }
        public int date { get; set; }
        public int no_search { get; set; }

        // Overloaded method
        // Getting lyrics from choosen audio by lyrics_id
        public async Task<string> GetLyricsAsync(string token)
        {
            if (lyrics_id != 0)
            {
                var method = new Method("audio.getLyrics", token);

                var dataProvider = new DataProvider();

                var property = new Property("lyrics_id", lyrics_id.ToString());

                string json = await dataProvider
                    .GetJsonString(method, property);

                var jToken = JToken.Parse(json);
                var lyrics = jToken
                    .SelectToken("response")
                    .SelectToken("text")
                    .Value<string>();

                return lyrics;
            }

            return "There are no provided lyrics for this audio!";
        }

        // Non-static realization of Audio.Add method
        public async Task<int> AddAsync(string token, AddProperties addProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id",
                aid.ToString()));

            properties.Add(new Property("owner_id",
                owner_id.ToString()));

            if (addProperties != null)
            {
                if (addProperties.GroupId != null)
                {
                    properties.Add(new Property("group_id",
                        addProperties.GroupId.ToString()));
                }

                if (addProperties.AlbumId != null)
                {
                    properties.Add(new Property("album_id",
                        addProperties.AlbumId.ToString()));
                }
            }

            var method = new Method("audio.add", token);

            string json = await dataProvider
                .GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var returnedId = 
                int.Parse(jToken.SelectToken("response").ToString());

            return returnedId;
        }

        // Allows you to delete audio from your audiolist
        public async Task<int> DeleteAsync(string token)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id", aid.ToString()));

            properties.Add(new Property("owner_id", owner_id.ToString()));

            var method = new Method("audio.delete", token);

            string json = await dataProvider
                .GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var returnedValue = int.Parse(jToken.SelectToken("response").ToString());

            return returnedValue;
        }
    }
}
