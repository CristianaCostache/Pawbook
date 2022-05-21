using Microsoft.AspNetCore.Mvc;
using Pawbook.Models;
using Pawbook.Services.Interfaces;
using Pawbook.ViewModels;
using System.Diagnostics;

namespace Pawbook.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFeedItemService _feedItemService;

        public HomeController(ILogger<HomeController> logger, IFeedItemService feedItemService)
        {
            _logger = logger;
            _feedItemService = feedItemService;
        }

        public IActionResult Feed()
        {
            List<FeedItem> feedItems = _feedItemService.GetAll();
            return View(feedItems);
        }

        public IActionResult Profile(int userId, int isLoggedUser)
        {
            List<FeedItem> feedItems = _feedItemService.GetByUser(userId, isLoggedUser);
            return View(feedItems);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
