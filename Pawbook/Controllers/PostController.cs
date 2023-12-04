#nullable disable
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
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Post post)
        {
            var loggedInUserId = HttpContext.Session.GetInt32("LoggedInUserId");
            _postService.AddPost(post, loggedInUserId);

            return RedirectToAction("Feed", "Home");
        }

        public IActionResult Delete(int postId)
        {
            _postService.Delete(postId);

            return RedirectToAction("Feed", "Home");
        }
    }
}
