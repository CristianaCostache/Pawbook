using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public CommentService(IRepositoryWrapper repositoryWrapper, IPostService postService, IUserService userService)
        {
            _repositoryWrapper = repositoryWrapper;
            _postService = postService;
            _userService = userService;
        }

        public void AddComment(Comment comment, int postId, int loggedInUserId)
        {
            Post post = _postService.GetById(postId);
            User user = _userService.GetUserById(loggedInUserId);

            comment.UserId = user.UserId;
            comment.User = user;
            post.Comments!.Add(comment);

            _repositoryWrapper.PostRepository.Update(post);
            _repositoryWrapper.Save();
        }

        public void Update(Comment comment)
        {
            Comment dbComment = GetById(comment.CommentId);

            _repositoryWrapper.CommentRepository.Update(dbComment);
            _repositoryWrapper.Save();
        }

        public Comment GetById(int id)
        {
            return _repositoryWrapper.CommentRepository.FindByCondition(comment => comment.CommentId == id).First();
        }

        public int CountCommentsByPostId(int postId)
        {
            List<Comment> comments = GetCommentsByPostId(postId);

            return comments.Count;
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            List<Comment> comments = _repositoryWrapper.CommentRepository.FindByCondition(comment => comment.PostId == postId).ToList();
            foreach (Comment comment in comments)
            {
                User? user = _repositoryWrapper.UserRepository.FindByCondition(user => user.UserId == comment.UserId).FirstOrDefault();
                comment.User = user;
            }

            return comments;
        }
    }
}
