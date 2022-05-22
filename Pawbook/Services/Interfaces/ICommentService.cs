using Pawbook.Models;

namespace Pawbook.Services.Interfaces
{
    public interface ICommentService
    {
        void AddComment(Comment comment, int postId, int loggedInUserId);
        List<Comment> GetCommentsByPostId(int postId);
        int CountCommentsByPostId(int postId);
    }
}
