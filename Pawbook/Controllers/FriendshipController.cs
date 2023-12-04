using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pawbook.Models;
using Pawbook.Services.Interfaces;

namespace Pawbook.Controllers
{
    public class FriendshipController : Controller
    {
        private readonly IFriendshipService _friendshipService;

        public FriendshipController(IFriendshipService friendshipService)
        {
            _friendshipService = friendshipService;
        }

        public IActionResult Index(int userId)
        {
            List<Friendship> friendships = _friendshipService.GetFriendshipByUserId(userId);

            return View(friendships);
        }

        public IActionResult Create(int userId, int loggedInUserId)
        {
            _friendshipService.AddFriendship(userId, loggedInUserId);

            return RedirectToAction("Feed", "Home");
        }
    }
}
