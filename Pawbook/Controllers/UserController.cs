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
using Pawbook.ViewModels;

namespace Pawbook.Controllers
{
    public class UserController : Controller
    {
        private readonly PawbookContext _context;
        private readonly IUserService _userService;

        public UserController(PawbookContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: Users/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Users/Register
        [HttpPost]
        public IActionResult Register([FromForm] User user)
        {
            if (_userService.UserExist(user))
            {
                ModelState.AddModelError(string.Empty, "Email is already used");
                return View();
            }
            _userService.Register(user);

            return RedirectToAction("Feed", "Home");
        }

        // GET: Users/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Users/Login
        [HttpPost]
        public IActionResult Login([FromForm] User user)
        {
            if(!_userService.UserExist(user))
            {
                ModelState.AddModelError(string.Empty, "The email is not associated to a Pawbook account. Please register!");

                return View();
            }
            if (!_userService.PasswordMatch(user))
            {
                ModelState.AddModelError(string.Empty, "Wrong password. Try again!");

                return View();
            }
            User dbUser = _userService.GetUserByEmail(user.Email);

            HttpContext.Session.SetInt32("LoggedInUserId", dbUser.UserId);
            HttpContext.Session.SetString("LoggedInUserRole", dbUser.UserRole);

            return RedirectToAction("Feed", "Home");
        }

        // POST: Users/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Feed", "Home");
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, [FromForm] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }
            _userService.Update(user);

            return RedirectToAction("Feed", "Home");
        }

        [HttpPost]
        public IActionResult Feed([FromForm] string searchString)
        {
            var users = _userService.GetUserByName(searchString);

            return View(users);
        }

        public async Task<IActionResult> Search()
        {
            return View(await _context.Users.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Search([FromForm] SearchItem searchItem)
        {
            var users = from user in _context.Users
                        select user;

            if (!String.IsNullOrEmpty(searchItem.searchTerm))
            {
                users = users.Where(s => s.Name!.Contains(searchItem.searchTerm)
                                    || s.OwnerName!.Contains(searchItem.searchTerm));
            }

            return View(await users.ToListAsync());
        }
    }
}
