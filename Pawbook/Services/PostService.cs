using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class PostService : IPostService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IWebHostEnvironment _webHostEnvironment;

        public PostService(IRepositoryWrapper repositoryWrapper, IWebHostEnvironment webHostEnvironment)
        {
            _repositoryWrapper = repositoryWrapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public void AddPost(Post post)
        {
            addImage(post);
            User user = _repositoryWrapper.UserRepository.FindAll().FirstOrDefault();
            user.Posts.Add(post);

            _repositoryWrapper.UserRepository.Update(user);
            _repositoryWrapper.Save();
        }

        public Post GetById(int id)
        {
            Post post = _repositoryWrapper.PostRepository.FindByCondition(post => post.PostId == id).FirstOrDefault();
            return post;
        }

        public List<Post> GetByUserId(int userId)
        {
            List<Post> posts = _repositoryWrapper.PostRepository.FindByCondition(post => post.UserId == userId).ToList();
            return posts;
        }

        private void addImage(Post post)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(post.ImageFile.FileName);
            string extension = Path.GetExtension(post.ImageFile.FileName);
            post.ImageName = fileName = fileName + DateTime.Now.ToString("_yyMMddHHmmss") + extension;
            string path = Path.Combine(wwwRootPath + "/img/post/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                post.ImageFile.CopyTo(fileStream);
            }
        }
    }
}
