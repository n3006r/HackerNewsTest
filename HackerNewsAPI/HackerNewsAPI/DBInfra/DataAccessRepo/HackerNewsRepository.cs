using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using HackerNewsAPI.DBInfra.InterfaceRepo;
using Newtonsoft.Json;
using HackerNewsAPI.DBInfra.ModelRepo;

namespace HackerNewsAPI.DBInfra.DataAccessRepo
{
    public class HackerNewsRepository : IHackerNewsRepository
    {
        private static HttpClient client;

        public HackerNewsRepository()
        {
            client = new HttpClient();
        }

        public async Task<List<int>> BestStoriesAsync()
        {
            var response = await client.GetAsync("https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty");
            var storyIds = new List<int>();

            if (response.IsSuccessStatusCode)
            {
                var storiesResponse = response.Content.ReadAsStringAsync().Result;
                storyIds = JsonConvert.DeserializeObject<List<int>>(storiesResponse);
            }

            return storyIds;
        }

        public async Task<HackerNewsStory> GetStoryByIdAsync(int storyID)
        {
            HackerNewsStory NewsStory = new HackerNewsStory();
            var response = await client.GetAsync(string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json", storyID));
            if (response.IsSuccessStatusCode)
            {
                var storyResponse = response.Content.ReadAsStringAsync().Result;
                NewsStory = JsonConvert.DeserializeObject<HackerNewsStory>(storyResponse);
            }

            return NewsStory;
        }
    }
}