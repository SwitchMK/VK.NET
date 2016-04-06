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
        public int id { get; set; }
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
        public async static Task<List<Audio>> Get(string token)
        {
            var method = new Method("audio.get", token);
            var dataProvider = new DataProvider();
            string json = await dataProvider.GetJsonString(method);

            JToken jToken = JToken.Parse(json);
            var audioList = jToken["response"].Children()
                .Skip(0)
                    .Select(c => c.ToObject<Audio>()).ToList();

            return audioList;
        }

        // Overloaded method
        // Static! Getting lyrics from choosen audio by lyrics_id
        public async static Task<string> GetLyrics(string token, string lyrics_id)
        {
            if (int.Parse(lyrics_id) != 0)
            {
                var method = new Method("audio.getLyrics", token);
                var dataProvider = new DataProvider();
                var property = new Property("lyrics_id", lyrics_id);
                string json = await dataProvider.GetJsonString(method, property);

                var jToken = JToken.Parse(json);
                var lyrics = jToken.SelectToken("response").SelectToken("text").Value<string>();

                return lyrics;
            }

            return "There are no provided lyrics for this audio!";
        }

        // Overloaded method
        // Getting lyrics from choosen audio by lyrics_id
        public async Task<string> GetLyrics(string token)
        {
            if (lyrics_id != 0)
            {
                var method = new Method("audio.getLyrics", token);
                var dataProvider = new DataProvider();
                var property = new Property("lyrics_id", lyrics_id.ToString());
                string json = await dataProvider.GetJsonString(method, property);

                var jToken = JToken.Parse(json);
                var lyrics = jToken.SelectToken("response").SelectToken("text").Value<string>();

                return lyrics;
            }

            return "There are no provided lyrics for this audio!";
        }

        // Getting audiolist by passing needed id 
        // like {owner_id}_{audio_id}
        public static async Task<List<Audio>> GetById(string token, params string[] audios)
        {
            if (audios.Length != 0)
            {
                var method = new Method("audio.getById", token);
                var dataProvider = new DataProvider();
                var property = new Property("audios", String.Join(",", audios));
                string json = await dataProvider.GetJsonString(method, property);

                var jToken = JToken.Parse(json);

                var partialAudiosList = jToken.SelectToken("response").
                    Children().
                        Skip(0).
                            Select(a => a.ToObject<Audio>()).ToList();

                return partialAudiosList;
            }

            throw new Exception();
        }
    }
}
