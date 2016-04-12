using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace VK.NET
{
    public partial class Audio
    {
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

        // Search method realization
        public static async Task<List<Audio>> SearchAsync(string token,
            SearchProperties search)
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

        public static async Task<int> EditAsync(string token, int ownerId, 
            int audioId, EditProperties editProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("owner_id", 
                ownerId.ToString()));

            properties.Add(new Property("audio_id",
                audioId.ToString()));

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

                properties.Add(new Property("genre_id", ((int)editProperties.Genre).ToString()));

                properties.Add(new Property("no_search", (editProperties.NoSearch ? 1 : 0).ToString()));
            }

            var method = new Method("audio.edit", token);

            var json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var result = int.Parse(jToken.SelectToken("response").ToString());

            return result;
        }

        public static async Task<int> ReorderAsync(string token, int audioId,
            ReorderProperties reorderProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id", audioId.ToString()));

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
        public static async Task<Audio> RestoreAsync(string token, 
            int audioId, int? ownerId = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_id", audioId.ToString()));

            if (ownerId != null)
            {
                properties.Add(new Property("owner_id", ownerId.ToString()));
            }

            var method = new Method("audio.restore", token);

            var json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var audioList = jToken.SelectToken("response")
                .Children()
                .Select(c => c.ToObject<Audio>())
                .ToList()[0];

            return audioList;
        }

        public static async Task<List<Album>> GetAlbumsAsync(string token, 
            int? ownerId = null, 
            int? offset = null,
            int? count = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            if (ownerId != null)
            {
                properties.Add(new Property("owner_id", ownerId.ToString()));
            }

            if (offset != null)
            {
                properties.Add(new Property("offset", offset.ToString()));
            }

            if (count != null)
            {
                properties.Add(new Property("count", count.ToString()));
            }

            var method = new Method("audio.getAlbums", token);

            var json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            var albumList = jToken.SelectToken("response")
                .Children()
                .Skip(1)
                .Select(c => c.ToObject<Album>())
                .ToList();

            return albumList;
        } 
    }
}
