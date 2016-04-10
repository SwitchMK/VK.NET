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

        // Allows you to edit some info about audio
        public async Task<int> EditAsync(string token, EditProperties editProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("owner_id",
                owner_id.ToString()));

            properties.Add(new Property("audio_id",
                aid.ToString()));

            if (editProperties != null)
            {
                if (!String.IsNullOrEmpty(editProperties.Artist))
                {
                    properties.Add(new Property("artist",
                        editProperties.Artist));
                }

                if (!String.IsNullOrEmpty(editProperties.Title))
                {
                    properties.Add(new Property("title",
                        editProperties.Title));
                }

                properties.Add(new Property("text", editProperties.Text));

                properties.Add(new Property("genre_id", 
                    ((int)editProperties.Genre).ToString()));

                properties.Add(new Property("no_search", 
                    (editProperties.NoSearch ? 1 : 0).ToString()));
            }

            var method = new Method("audio.edit", token);

            var json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var result = int.Parse(jToken.SelectToken("response").ToString());

            return result;
        }

        // Allows you ti change order of audios withing audiolist
        public async Task<int> ReorderAsync(string token, 
            ReorderProperties reorderProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id", aid.ToString()));

            if (reorderProperties != null)
            {
                if (reorderProperties.OwnerId != null)
                {
                    properties.Add(new Property("owner_id",
                        reorderProperties.OwnerId.ToString()));
                }

                if (reorderProperties.Before != null)
                {
                    properties.Add(new Property("before",
                        reorderProperties.Before.ToString()));
                }

                if (reorderProperties.After != null)
                {
                    properties.Add(new Property("after",
                        reorderProperties.After.ToString()));
                }
            }

            var method = new Method("audio.reorder", token);

            var json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var result = int.Parse(jToken.SelectToken("response").ToString());

            return result;
        }

        // Allows you to restore audio if it was accidently deleted
        public async Task<Audio> RestoreAsync(string token)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id", aid.ToString()));

            properties.Add(new Property("owner_id", owner_id.ToString()));

            var method = new Method("audio.restore", token);

            var json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var audioList = jToken.SelectToken("response")
                .Children()
                .Select(c => c.ToObject<Audio>())
                .ToList()[0];

            return audioList;
        }
    }
}
