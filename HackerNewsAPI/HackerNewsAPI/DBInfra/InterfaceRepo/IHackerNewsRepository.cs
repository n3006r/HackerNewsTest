
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace HackerNewsAPI.DBInfra.InterfaceRepo
{
    public interface IHackerNewsRepository
    {
        Task<HttpResponseMessage> BestStoriesAsync();
        Task<HttpResponseMessage> GetStoryByIdAsync(int id);

    }
}