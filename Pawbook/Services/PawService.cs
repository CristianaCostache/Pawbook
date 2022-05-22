using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class PawService : IPawService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IPostService _postService;
        private IUserService _userService;

        public PawService(IRepositoryWrapper repositoryWrapper, IPostService postService, IUserService userService)
        {
            _repositoryWrapper = repositoryWrapper;
            _postService = postService;
            _userService = userService;
        }

        public void AddPaw(int postId, int loggedInUserId)
        {
            Post post = _postService.GetById(postId);
            User user = _userService.GetUserById(loggedInUserId);

            Paw paw = new Paw();
            paw.UserId = user.UserId;
            paw.User = user;
            post.Paws.Add(paw);

            _repositoryWrapper.PostRepository.Update(post);
            _repositoryWrapper.Save();
        }

        public int CountPawsByPostId(int postId)
        {
            List<Paw> paws = GetPawsByPostId(postId);
            return paws.Count();
        }

        public List<Paw> GetPawsByPostId(int postId)
        {
            List<Paw> paws = _repositoryWrapper.PawRepository.FindByCondition(paw => paw.PostId == postId).ToList();
            foreach (Paw paw in paws)
            {
                User user = _repositoryWrapper.UserRepository.FindByCondition(user => user.UserId == paw.UserId).FirstOrDefault();  // this will be modified
                paw.User = user;
            }
            return paws;
        }

        public bool IsPawedByUser(int postId, int userId)
        {
            Paw paw = _repositoryWrapper.PawRepository.FindByCondition(paw => paw.PostId == postId && paw.UserId == userId).FirstOrDefault();
            if(paw == null)
            {
                return false;
            }
            return true;
        }
    }
}
