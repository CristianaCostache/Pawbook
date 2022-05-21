using Pawbook.Models;

namespace Pawbook.Services.Interfaces
{
    public interface IPostService
    {
        void AddPost(Post post);
        Post GetById(int id);
        List<Post> GetByUserId(int userId);
    }
}
