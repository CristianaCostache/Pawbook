using Pawbook.Models;
using Pawbook.Repositories.Intrfaces;
using Pawbook.Services.Interfaces;

namespace Pawbook.Services
{
    public class FriendshipService : IFriendshipService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public FriendshipService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
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
                User? friend = _repositoryWrapper.UserRepository.FindByCondition(item => item.UserId == friendship.FriendId).FirstOrDefault();
                friendship.Friend = friend;
            }

            return friendships;
        }

        public bool IsFriendWith(int loggedInUserId, int userId)
        {
            Friendship? friendship = _repositoryWrapper.FriendshipRepository.FindByCondition(item => item.UserId == loggedInUserId && item.FriendId == userId).FirstOrDefault();

            return friendship != null;
        }
    }
}
