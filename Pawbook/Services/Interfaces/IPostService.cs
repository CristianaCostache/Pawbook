using Pawbook.Models;

namespace Pawbook.Services.Interfaces
{
    public interface IPostService
    {
        void AddPost(Post post, int? loggedInUserId);
        Post GetById(int id);
        List<Post> GetByUserId(int userId);
    }
}
