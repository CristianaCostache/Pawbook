using Microsoft.AspNetCore.Mvc;
using Pawbook.Models;
using Pawbook.Services.Interfaces;

namespace Pawbook.Controllers
{
    public class CommentController : Controller
    {
        private readonly PawbookContext _context;
        private readonly ICommentService _commentService;

        public CommentController(PawbookContext context, ICommentService commentService)
        {
            _context = context;
            _commentService = commentService;
        }

        public IActionResult Index(int postId)
        {
            var countComments = _commentService.CountCommentsByPostId(postId);
            if(countComments > 0)
            {
                List<Comment> comments = _commentService.GetCommentsByPostId(postId);
                return View(comments);
            }
            return new EmptyResult();
        }

        public IActionResult Create(int postId)
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] Comment comment, int postId, int loggedInUserId)
        {
            _commentService.AddComment(comment, postId, loggedInUserId);
            return RedirectToAction("Feed", "Home");
        }
    }
}
