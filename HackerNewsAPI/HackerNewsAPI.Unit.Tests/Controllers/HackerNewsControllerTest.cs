using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNewsAPI.Controllers;
using HackerNewsAPI.DBInfra.ModelRepo;
using HackerNewsAPI.DBInfra.InterfaceRepo;
using HackerNewsAPI.DBInfra.DataAccessRepo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace hackernewsapi.unit.tests.Controllers
{
    [TestClass]
    public class HackerNewsControllerTest
    {
        private HackerNewsController _hackerNewsController;
        private HackerNewsRepository _mockHackerNewsRepository;

        public HackerNewsControllerTest()
        {
            #region 
            //https://stackoverflow.com/questions/38318247/mock-imemorycache-in-unit-test
            #endregion
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();
            var memoryCache = serviceProvider.GetService<IMemoryCache>();

            _mockHackerNewsRepository = new HackerNewsRepository();
            _hackerNewsController = new HackerNewsController(memoryCache, _mockHackerNewsRepository);
        }

        [TestMethod]
        public async Task OnGet_Default_ReturnsStories()
        {
            //Arrange
            var emptyList = await _mockHackerNewsRepository.BestStoriesAsync();

            //Act
            var stories = await _hackerNewsController.Index("");

            //Asset
            Assert.IsNotNull(stories);
            Assert.AreEqual(emptyList.Count(), stories.Count());
        }

        [TestMethod]
        public async Task OnGet_Default_ReturnsEmtpyListFor_NonSearchable_Story()
        {
            //Arrange + Act
            var stories = await _hackerNewsController.Index("$$$$ This should not be present $$$$");

            //Asset
            Assert.IsNotNull(stories);
            Assert.AreEqual(0, stories.Count());
        }

        [TestMethod]
        public async Task OnGet_Default_Returns_NonEmptylist_ForSearchedStory()
        {
            //Arrange + Act
            var stories = await _hackerNewsController.Index("search");

            bool isDataPresent = stories.Count > 0 ? true : false;
            //Asset
            Assert.IsNotNull(stories);
            Assert.IsTrue(isDataPresent);
        }
    }
}
