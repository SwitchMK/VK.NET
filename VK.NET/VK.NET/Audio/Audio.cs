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
    public class Audio
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

        // Getting adios from json and pushing to code
        // with collection.
        public async static Task<List<Audio>> GetAsync(string token, 
            GetProperties getProperties = null)
        {
            string json;

            var dataProvider = new DataProvider();

            var method = new Method("audio.get", token);

            if (getProperties != null)
            {
                var properties = new List<Property>();

                if (getProperties.OwnerId != null)
                {
                    properties.Add(new Property("owner_id",
                        getProperties.OwnerId.ToString()));
                }

                if (getProperties.AlbumId != null)
                {
                    properties.Add(new Property("album_id",
                        getProperties.AlbumId.ToString()));
                }

                if (getProperties.AudioIds != null &&
                    getProperties.AudioIds.Length > 0)
                {
                    properties.Add(new Property("audio_ids",
                        String.Join(",", getProperties.AudioIds)));
                }

                properties.Add(new Property("need_user",
                    (getProperties.NeedUser ? 1 : 0).ToString()));

                if (getProperties.Offset > 0)
                {
                    properties.Add(new Property("offset",
                        getProperties.Offset.ToString()));
                }

                if (getProperties.Count != null)
                {
                    properties.Add(new Property("count",
                        getProperties.Count.ToString()));
                }

                json = await dataProvider.GetJsonString(method, 
                    properties.ToArray());
            }
            else
            {
                json = await dataProvider.GetJsonString(method);
            }

            JToken jToken = JToken.Parse(json);
            var audioList = jToken["response"]
                .Children()
                .Select(c => c.ToObject<Audio>())
                .ToList();

            return audioList;
        }

        // Overloaded method
        // Static! Getting lyrics from choosen audio by lyrics_id
        public async static Task<string> GetLyricsAsync(string token, string lyrics_id)
        {
            if (int.Parse(lyrics_id) != 0)
            {
                var method = new Method("audio.getLyrics", token);
                var dataProvider = new DataProvider();
                var property = new Property("lyrics_id", lyrics_id);
                string json = await dataProvider.GetJsonString(method, property);

                var jToken = JToken.Parse(json);
                var lyrics = jToken
                    .SelectToken("response")
                    .SelectToken("text")
                    .Value<string>();

                return lyrics;
            }

            return "There are no provided lyrics for this audio!";
        }

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

        // Getting audiolist by passing needed id 
        // like {owner_id}_{audio_id}
        public static async Task<List<Audio>> GetByIdAsync(string token, 
            params GetByIdProperties[] getByIdProperties)
        {
            if (getByIdProperties.Length != 0)
            {
                var method = new Method("audio.getById", token);

                var dataProvider = new DataProvider();

                var property = new Property("audios", 
                    String.Join(",", getByIdProperties));

                string json = await dataProvider
                    .GetJsonString(method, property);

                var jToken = JToken.Parse(json);

                var partialAudiosList = jToken
                    .SelectToken("response")
                    .Children()
                    .Select(a => a.ToObject<Audio>())
                    .ToList();

                return partialAudiosList;
            }

            throw new Exception();
        }

        // Search method realization
        public static async Task<List<Audio>> SearchAsync(string token, 
            Search search)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties
                .Add(new Property("q", search.TextRequest));

            properties.Add(new Property("auto_complete", 
                    (search.AutoComplete ? 1 : 0).ToString()));

            properties.Add(new Property("lyrics", 
                    (search.WithLyrics ? 1 : 0).ToString()));

            properties.Add(new Property("performer_only", 
                    (search.PerformerOnly ? 1 : 0).ToString()));

            properties.Add(new Property("sort", 
                ((int)search.SortMean).ToString()));

            properties.Add(new Property("search_own", 
                    (search.SearchOwn ? 1 : 0).ToString()));

            properties.Add(new Property("offset", search.Offset.ToString()));

            properties.Add(new Property("count", search.Count.ToString()));

            var method = new Method("audio.search", token);

            string json = await dataProvider
                .GetJsonString(method, properties.ToArray());

            JToken jToken = JToken.Parse(json);

            var audioList = jToken
                .SelectToken("response")
                .Skip(1)
                .Select(c => c.ToObject<Audio>())
                .ToList();

            return audioList;
        }

        // Static realization of Add method which allows you 
        // to add audio to your Vkontakte audiolist.
        public static async Task<int> AddAsync(string token, int audioId, 
            int ownerId, AddProperties addProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id", 
                audioId.ToString()));

            properties.Add(new Property("owner_id",
                ownerId.ToString()));

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

        // Overloaded static Audio.Delete method
        public static async Task<int> DeleteAsync(string token, int audioId, int ownerId)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id", audioId.ToString()));

            properties.Add(new Property("owner_id", ownerId.ToString()));

            var method = new Method("audio.delete", token);

            string json = await dataProvider
                .GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var returnedValue = 
                int.Parse(jToken.SelectToken("response").ToString());

            return returnedValue;
        }
    }
}
