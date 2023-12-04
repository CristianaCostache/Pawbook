using Microsoft.AspNetCore.Mvc;
using Pawbook.Models;
using Pawbook.Services.Interfaces;
using Pawbook.ViewModels;
using System.Diagnostics;

namespace Pawbook.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFeedItemService _feedItemService;

        public HomeController(IFeedItemService feedItemService)
        {
            _feedItemService = feedItemService;
        }

        public IActionResult Feed()
        {
            if (HttpContext.Session.GetInt32("LoggedInUserId") != null)
            {
                var loggedInUserId = HttpContext.Session.GetInt32("LoggedInUserId");
                List<FeedItem> feedItems = _feedItemService.GetAll(loggedInUserId);

                return View(feedItems);
            }

            return RedirectToAction("Login", "User");
        }

        public IActionResult Profile(int userId, int isLoggedUser)
        {
            var loggedInUserId = HttpContext.Session.GetInt32("LoggedInUserId");
            List<FeedItem> feedItems = _feedItemService.GetByUser(userId, loggedInUserId, isLoggedUser);

            return View(feedItems);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult Sounds()
        {
            return View();
        }

        public IActionResult Compressing()
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
