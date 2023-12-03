using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using HackerNewsAPI.DBInfra.InterfaceRepo;
using HackerNewsAPI.DBInfra.ModelRepo;

namespace HackerNewsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HackerNewsController : ControllerBase
    {
        private IMemoryCache _cache;
        private readonly IHackerNewsRepository _repo;

        public HackerNewsController(IMemoryCache cache, IHackerNewsRepository repository)
        {
            this._cache = cache;
            this._repo = repository;
        }

        public async Task<List<HackerNewsStory>> Index(string searchTerm)
        {
            List<HackerNewsStory> bestStories = new List<HackerNewsStory>();

            var storyIDs = await _repo.BestStoriesAsync();
            if (storyIDs != null && storyIDs.Count > 0)
            {
                var tasks = storyIDs.Select(GetStoryByIDAsync);
                bestStories = (await Task.WhenAll(tasks)).ToList();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var search = searchTerm.ToLower();
                    bestStories = bestStories.Where(s =>
                                       s.Title.ToLower().IndexOf(search) > -1 || s.By.ToLower().IndexOf(search) > -1)
                                       .ToList();
                }
            }

            return bestStories;
        }

        private async Task<HackerNewsStory> GetStoryByIDAsync(int storyId)
        {
            return await _cache.GetOrCreateAsync<HackerNewsStory>(storyId,
                async cacheEntry =>
                {
                    return await _repo.GetStoryByIdAsync(storyId);
                });
        }
    }
}
