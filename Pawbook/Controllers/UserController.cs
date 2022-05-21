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

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Email,Password,Name,OwnerName,Gender,Type,ImageName")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
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

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
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
