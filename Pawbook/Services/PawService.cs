using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class PawService : IPawService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

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

            Paw paw = CreatePaw(user);
            post.Paws!.Add(paw);

            _repositoryWrapper.PostRepository.Update(post);
            _repositoryWrapper.Save();
        }

        private static Paw CreatePaw(User user)
        {
            Paw paw = new Paw();
            paw.UserId = user.UserId;
            paw.User = user;

            return paw;
        }

        public int CountPawsByPostId(int postId)
        {
            List<Paw> paws = GetPawsByPostId(postId);

            return paws.Count;
        }

        public void Update(Paw paw)
        {
            Paw dbPaw = GetById(paw.PawId);
            UpdateDbPaw(dbPaw);
        }

        private void UpdateDbPaw(Paw dbPaw)
        {
            _repositoryWrapper.PawRepository.Update(dbPaw);
            _repositoryWrapper.Save();
        }

        private Paw GetById(int id)
        {
            return _repositoryWrapper.PawRepository.FindByCondition(paw => paw.PawId == id).First();
        }

        public List<Paw> GetPawsByPostId(int postId)
        {
            List<Paw> paws = _repositoryWrapper.PawRepository.FindByCondition(paw => paw.PostId == postId).ToList();
            foreach (Paw paw in paws)
            {
                User? user = _repositoryWrapper.UserRepository.FindByCondition(user => user.UserId == paw.UserId).FirstOrDefault();
                paw.User = user;
            }

            return paws;
        }

        public bool IsPawedByUser(int postId, int userId)
        {            
            Paw? paw = _repositoryWrapper.PawRepository.FindByCondition(paw => paw.PostId == postId && paw.UserId == userId).FirstOrDefault();

            return paw != null;
        }
    }
}
