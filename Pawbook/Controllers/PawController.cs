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
    public class PawController : Controller
    {
        private readonly PawbookContext _context;
        private readonly IPawService _pawService;

        public PawController(PawbookContext context, IPawService pawService)
        {
            _context = context;
            _pawService = pawService;
        }

        public IActionResult Index(int postId)
        {
            List<Paw> paws = _pawService.GetPawsByPostId(postId);
            return View(paws);
        }

        public IActionResult Create(int postId, int loggedInUserId)
        {
            _pawService.AddPaw(postId, loggedInUserId);
            return RedirectToAction("Feed", "Home");
        }
    }
}
