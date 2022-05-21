using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class CommentService : ICommentService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IPostService _postService;

        public CommentService(IRepositoryWrapper repositoryWrapper, IPostService postService)
        {
            _repositoryWrapper = repositoryWrapper;
            _postService = postService;
        }

        public void AddComment(Comment comment, int postId)
        {
            Post post = _postService.GetById(postId);
            User user = _repositoryWrapper.UserRepository.FindAll().FirstOrDefault(); // this will be modified

            comment.UserId = user.UserId;
            comment.User = user;
            post.Comments.Add(comment);

            _repositoryWrapper.PostRepository.Update(post);
            _repositoryWrapper.Save();
        }

        public int CountCommentsByPostId(int postId)
        {
            List<Comment> comments = GetCommentsByPostId(postId);
            return comments.Count();
        }

        public List<Comment> GetCommentsByPostId(int postId)
        {
            List<Comment> comments = _repositoryWrapper.CommentRepository.FindByCondition(comment => comment.PostId == postId).ToList();
            foreach (Comment comment in comments)
            {
                User user = _repositoryWrapper.UserRepository.FindByCondition(user => user.UserId == comment.UserId).FirstOrDefault();  // this will be modified
                comment.User = user;
            }
            return comments;
        }
    }
}
