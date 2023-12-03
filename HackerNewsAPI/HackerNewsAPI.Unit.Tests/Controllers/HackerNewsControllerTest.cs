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

namespace hackernewsapi.unit.tests.Controllers
{
    [TestClass]
    public class HackerNewsControllerTest
    {
        private HackerNewsController _hackerNewsController;
        private Mock<IHackerNewsRepository> _mockHackerNewsService;

        public HackerNewsControllerTest(){
            _mockHackerNewsService = new Mock<IHackerNewsRepository>();
            _hackerNewsController = new HackerNewsController(null, _mockHackerNewsService.Object);
            _hackerNewsController.ControllerContext = new ControllerContext();
            _hackerNewsController.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [TestMethod]
        public async Task  OnGet_Default_ReturnsEmtpyList()
        {
            //Arrange
            var emptyList = new List<int>();
            _mockHackerNewsService.Setup(s => s.BestStoriesAsync()).ReturnsAsync(emptyList);

            //Act
            var stories = await _hackerNewsController.Index("");

            //Asset
            Assert.IsNotNull(stories);
            Assert.AreEqual(emptyList.Count, stories.Count());
        }
    }
}
