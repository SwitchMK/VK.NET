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

            List<Audio> audioList = jToken
                .SelectToken("response")
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

                List<Audio> partialAudiosList = jToken
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
                string lyrics = jToken
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

            List<Audio> audioList = jToken
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

            int returnedId =
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

            int returnedValue =
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

                properties.Add(new Property("genre_id", 
                    ((int)editProperties.Genre).ToString()));

                properties.Add(new Property("no_search", 
                    (editProperties.NoSearch ? 1 : 0).ToString()));
            }

            var method = new Method("audio.edit", token);

            var json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            int result = int.Parse(jToken.SelectToken("response").ToString());

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

            int result = int.Parse(jToken.SelectToken("response").ToString());

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

            Audio audioList = jToken.SelectToken("response")
                .Children()
                .Select(c => c.ToObject<Audio>())
                .ToList()[0];

            return audioList;
        }

        // Allows you to get all albums
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

            var json = 
                await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            List<Album> albumList = jToken
                .SelectToken("response")
                .Children()
                .Skip(1)
                .Select(c => c.ToObject<Album>())
                .ToList();

            return albumList;
        }

        // Allows you to add new album
        public static async Task<int> AddAlbumAsync(string token, string title, 
            int? groupId = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("title", title));

            if (groupId != null)
            {
                properties.Add(new Property("group_id", groupId.ToString()));
            }

            var method = new Method("audio.addAlbum", token);

            string json = 
                await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            int result = int.Parse(jToken.SelectToken("response")
                .ToString());

            return result;
        }

        // Allows you to edit album (e.g. change the title)
        public static async Task<int> EditAlbumAsync(string token, 
            string title, int albumId,
            int? groupId = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("album_id", albumId.ToString()));

            properties.Add(new Property("title", title));

            if (groupId != null)
            {
                properties.Add(new Property("group_id", groupId.ToString()));
            }

            var method = new Method("audio.editAlbum", token);

            string json = 
                await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            int result = int.Parse(jToken.SelectToken("response")
                .ToString());

            return result;
        }

        // Allows you to remove needed albums by album_id
        public static async Task<int> DeleteAlbumAsync(string token,
          int albumId, int? groupId = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("album_id", albumId.ToString()));

            if (groupId != null)
            {
                properties.Add(new Property("group_id", groupId.ToString()));
            }

            var method = new Method("audio.deleteAlbum", token);

            string json = 
                await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            int result = int.Parse(jToken.SelectToken("response")
                .ToString());

            return result;
        }

        // Allows you to move chosen audio to another album
        public static async Task<int> MoveToAlbumAsync(string token,
            MoveToAlbumProperties moveToAlbumProperties)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            properties.Add(new Property("audio_ids",
                String.Join(",", moveToAlbumProperties.AudioIds)));

            if (moveToAlbumProperties.AlbumId != null)
            {
                properties.Add(new Property("album_id", 
                    moveToAlbumProperties.AlbumId.ToString()));
            }

            if (moveToAlbumProperties.GroupId != null)
            {
                properties.Add(new Property("group_id",
                    moveToAlbumProperties.GroupId.ToString()));
            }

            var method = new Method("audio.moveToAlbum", token);

            string json = await dataProvider.GetJsonString(method, properties.ToArray());

            var jToken = JToken.Parse(json);

            int result = int.Parse(jToken
                .SelectToken("response").ToString());

            return result;
        }

        // Provide recommendations which are similar to you audioList
        public static async Task<List<Audio>> GetRecommentationsAsync(string token, 
            GetRecommendationsProperties getRecommendationsProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            var method = new Method("audio.getRecommendations", token);

            string json;

            if (getRecommendationsProperties != null)
            {
                if (getRecommendationsProperties.TargetAudio != null)
                {
                    properties.Add(new Property("target_audio",
                        getRecommendationsProperties.TargetAudio.ToString()));
                }

                if (getRecommendationsProperties.UserId != null)
                {
                    properties.Add(new Property("user_id",
                        getRecommendationsProperties.UserId.ToString()));
                }

                properties.Add(new Property("offset",
                    getRecommendationsProperties.Offset.ToString()));

                properties.Add(new Property("count",
                    getRecommendationsProperties.Count.ToString()));

                properties.Add(new Property("shuffle",
                    (getRecommendationsProperties.Shuffle ? 1 : 0)
                    .ToString()));

                json = await dataProvider.GetJsonString(method, properties.ToArray());
            }
            else
            {
                json = await dataProvider.GetJsonString(method);
            }

            var jToken = JToken.Parse(json);

            List<Audio> audioList = jToken
               .SelectToken("response")
               .Select(c => c.ToObject<Audio>())
               .ToList();

            return audioList;
        }

        // Provide what is the most popular at the moment
        public static async Task<List<Audio>> GetPopularAsync(string token, 
            GetPopularProperties getPopularProperties = null)
        {
            var dataProvider = new DataProvider();

            var properties = new List<Property>();

            var method = new Method("audio.getPopular", token);

            string json;

            if (getPopularProperties != null)
            {
                properties.Add(new Property("only_eng",
                    (getPopularProperties.EnglishOnly ? 1 : 0).ToString()));

                properties.Add(new Property("genre_id",
                    ((int)(getPopularProperties.Genre)).ToString()));

                properties.Add(new Property("offset",
                    getPopularProperties.Offset.ToString()));

                properties.Add(new Property("count",
                    getPopularProperties.Count.ToString()));

                json = await dataProvider
                    .GetJsonString(method, properties.ToArray());
            }
            else
            {
                json = await dataProvider.GetJsonString(method);
            }

            var jToken = JToken.Parse(json);

            List<Audio> audioList = jToken
                .SelectToken("response")
                .Children()
                .Select(c => c.ToObject<Audio>())
                .ToList();

            return audioList;
        }

        // Total amount of audios in user's or group's audiolist
        public static async Task<int> GetCountAsync(string token, int ownerId)
        {
            var dataProvider = new DataProvider();

            var property = new Property("owner_id", ownerId.ToString());

            var method = new Method("audio.getCount", token);

            string json = await dataProvider.GetJsonString(method, property);

            var jToken = JToken.Parse(json);

            int result = int.Parse(jToken
                .SelectToken("response").ToString());

            return result;
        }
    }
}
