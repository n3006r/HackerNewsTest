
using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNewsAPI.DBInfra.ModelRepo;

namespace HackerNewsAPI.DBInfra.InterfaceRepo
{
    public interface IHackerNewsRepository
    {
        Task<List<int>> BestStoriesAsync();
        Task<HackerNewsStory> GetStoryByIdAsync(int id);
    }
}