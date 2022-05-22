using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class FriendshipService : IFriendshipService
    {
        private IRepositoryWrapper _repositoryWrapper;
        private IUserService _userService;

        public FriendshipService(IRepositoryWrapper repositoryWrapper, IUserService userService)
        {
            _repositoryWrapper = repositoryWrapper;
            _userService = userService;
        }

        public void AddFriendship(int userId, int loggedInUserId)
        {
            Friendship friendship = new Friendship();
            friendship.FriendId = userId;
            friendship.UserId = loggedInUserId;

            _repositoryWrapper.FriendshipRepository.Create(friendship);
            _repositoryWrapper.Save();
        }

        public List<Friendship> GetFriendshipByUserId(int userId)
        {
            List<Friendship> friendships = _repositoryWrapper.FriendshipRepository.FindByCondition(item => item.UserId == userId).ToList();
            foreach(Friendship friendship in friendships)
            {
                User friend = _repositoryWrapper.UserRepository.FindByCondition(item => item.UserId == friendship.FriendId).FirstOrDefault();
                friendship.Friend = friend;
            }
            return friendships;
        }

        public bool IsFriendWith(int userId)
        {
            Friendship friendship = _repositoryWrapper.FriendshipRepository.FindByCondition(item => item.FriendId == userId).FirstOrDefault();
            if (friendship == null)
            {
                return false;
            }
            return true;
        }
    }
}
