using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;

        public PostService(IRepositoryWrapper repositoryWrapper, IWebHostEnvironment webHostEnvironment, IUserService userService)
        {
            _repositoryWrapper = repositoryWrapper;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public void AddPost(Post post, int? loggedInUserId)
        {
            addImage(post);
            User user = _userService.GetUserById((int)loggedInUserId!);
            post.Status = Post.POST_STATUS_AVAILABLE;
            user.Posts!.Add(post);

            _repositoryWrapper.UserRepository.Update(user);
            _repositoryWrapper.Save();
        }

        public void Delete(int postId)
        {
            Post post = GetById(postId);
            post.Status = Post.POST_STATUS_DELETED;

            UpdateDbPost(post);
        }

        public Post GetById(int id)
        {
            return _repositoryWrapper.PostRepository.FindByCondition(post => post.PostId == id).First();
        }

        public List<Post> GetByUserId(int userId)
        {
            return _repositoryWrapper.PostRepository.FindByCondition(post => post.UserId == userId && post.Status == Post.POST_STATUS_AVAILABLE).ToList();
        }

        private void addImage(Post post)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string fileName = Path.GetFileNameWithoutExtension(post.ImageFile!.FileName);
            string extension = Path.GetExtension(post.ImageFile.FileName);
            post.ImageName = fileName = fileName + DateTime.Now.ToString("_yyMMddHHmmss") + extension;
            string path = Path.Combine(wwwRootPath + "/img/post/", fileName);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                post.ImageFile.CopyTo(fileStream);
            }
        }

        public void Update(Post post)
        {
            Post dbPost = GetById(post.PostId);
            UpdateDbPost(dbPost);
        }

        private void UpdateDbPost(Post dbPost)
        {
            _repositoryWrapper.PostRepository.Update(dbPost);
            _repositoryWrapper.Save();
        }
    }
}
